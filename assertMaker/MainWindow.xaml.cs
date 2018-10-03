using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace assertMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public string strPath = @"S:\assertMaker\assertMaker\logs\Log.txt";
        string sqlQuery;
        public string createTemptable;
        public string getColumnNamesQuery;
        public string getRowsCountQuery;
        public SqlConnection connection;
        int capacity;
        int numberOfRowsTempTable;
        bool _assertTypeCheck;

        List<object> queryList = new List<object>();
        List<object> columnNamesFromQuery = new List<object>();
       // List<object> tempList3 = new List<object>();
        List<object> tempListRowsCount = new List<object>();

        //save exception to log.txt
       
        public void DbConnection()
        {
            connection = new SqlConnection(@"Data source=192.168.115.156; database=CRS_QA; User id=RescoUser; Password=res23co;");
            connection.Open();
        }
        public List<object> ExecuteQueryAndAddDataToList(string query)
        {
            List<object> _tempList = new List<object>();
            _tempList.Clear();
            string doQuery = query;
            int iterator = 0;
            using (SqlCommand command = new SqlCommand(doQuery, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {                    
                    while (reader.Read())
                    {
                        _tempList.Add(reader.GetValue(0));
                        iterator++;
                    }                    
                    reader.Close();
                    reader.Dispose();
                    command.Dispose();
                }
            }
            return _tempList;
        }
        public void ExecuteQuery(string query)
        {
            string doQuery = query;
            SqlCommand command = new SqlCommand(doQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            reader.Dispose();
            reader.Close();
            command.Dispose();
        }
        public List<object> FillListXTimes(string query, int capacity)
        {
            List<object> _tempList = new List<object>();
            _tempList.Clear(); 
            string doQuery = query;
            using (SqlCommand command = new SqlCommand(doQuery, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < capacity; i++) //capacity only for one row --> need to get number of rows /SELECT COUNT(*) FROM tempTable/ capacity*rowsCount ->ExecuteReader reads only first row...
                        {
                            _tempList.Add(reader.GetSqlValue(i));
                        }
                    }
                    reader.Close();
                    reader.Dispose();
                    command.Dispose();
                }
            }
            return _tempList;
        }

        public void DropTestTable()
        {
            string dropQuery = " DROP TABLE tempTable ";
            SqlCommand SQLcommand = connection.CreateCommand();
            SQLcommand.CommandText = dropQuery;
            SqlDataReader reader = SQLcommand.ExecuteReader();
            SQLcommand.Dispose();
            reader.Close();
        }
        public bool LookForOrderByInQuery(string queryToCheck)
        {
            bool IsOrder;
            string trimmedQuery= null;
            int queryLength = queryToCheck.Length;
            trimmedQuery = queryToCheck.Remove(0, queryLength - 40);
            if (trimmedQuery.Contains("ORDER"))
                IsOrder = true;
            else
                IsOrder = false;
            return IsOrder;
        }
        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                printAssertTextBox.Clear();
                printAssertTextBox.Text = "Executing query... Please wait";
                sqlQuery = new TextRange(getQueryRichTextBox.Document.ContentStart, getQueryRichTextBox.Document.ContentEnd).Text;
                bool OrderByExists = LookForOrderByInQuery(sqlQuery);
                if (OrderByExists == false)
                    createTemptable = "SELECT * INTO tempTable FROM (" + sqlQuery + ") AS virtual_table_name"; //FIXED
                else
                    createTemptable = "SELECT * INTO tempTable FROM (" + sqlQuery + " OFFSET 0 ROWS ) AS virtual_table_name"; //FIXED

                getColumnNamesQuery = " SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'tempTable' ";
                getRowsCountQuery = " SELECT COUNT(*) FROM tempTable ";

                DbConnection();
                try {
                    DropTestTable();
                    }
                catch (Exception drop) {
                    Loger.ErrorLogging(drop, strPath);
                }
                ExecuteQuery(createTemptable);
                columnNamesFromQuery = ExecuteQueryAndAddDataToList(getColumnNamesQuery);
                tempListRowsCount = ExecuteQueryAndAddDataToList(getRowsCountQuery);
                numberOfRowsTempTable = int.Parse(tempListRowsCount[0].ToString());
                if (numberOfRowsTempTable == 1)
                {
                    capacity = columnNamesFromQuery.Count;
                    queryList = FillListXTimes(sqlQuery, capacity);
                    DropTestTable();
                    connection.Close();
                    string completeAssertion;
                    if (_assertTypeCheck == true)
                    {
                        printAssertTextBox.Clear();
                        completeAssertion = AssertCreator.CreateAssertion2(queryList);
                        printAssertTextBox.Text = AssertCreator.CreateAssertion2(queryList);
                    }
                    else
                    {
                        printAssertTextBox.Clear();
                        completeAssertion = AssertCreator.CreateAssertion(queryList, columnNamesFromQuery);
                        printAssertTextBox.Text = AssertCreator.CreateAssertion(queryList, columnNamesFromQuery);
                    }
                    string textWithNewLinesMarkups = completeAssertion.Replace("\n", "\n" + System.Environment.NewLine);
                    Loger.AssertionLogging(textWithNewLinesMarkups, strPath);
                }
                else if (numberOfRowsTempTable == 0)
                {
                    connection.Close();
                    printAssertTextBox.Clear();
                    printAssertTextBox.Text = "Query do not return any results!";
                }
                else
                {
                    connection.Close();
                    printAssertTextBox.Clear();
                    printAssertTextBox.Text = "Too many (" + numberOfRowsTempTable + ") rows in query result. Modify or add conditions to query and try again.";
                }
            }
            catch (Exception exc)
            {
                printAssertTextBox.Text = exc.ToString();
                Loger.ErrorLogging(exc, strPath);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }
        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedTypeOfAssertion = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content.ToString();
            if (selectedTypeOfAssertion == "oDtb.Rows(0).ItemArray(1))")
                _assertTypeCheck = true;
            else
                _assertTypeCheck = false;
        }
    }
}
