   M S B A M L   `   `   `  ����F  BassertMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:assertMakerassertMaker  5   	       assertMaker.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v17.1, Version=17.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:assertMaker     5   	   ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5   	     � Title$  Assertion Maker��5	   	   $��723.948��6!   $��804.292��62    � 
ResizeMode$ NoResize=�6B   .��5
      � $	��693��6   $	��Top=�6   $��0,0,2,0q�60   .+�5   
   �� -   5         ��Name$ printAssertTextBox��5      $
��Left=�6.   $	��612��6I   $��
427,71,0,0q�6V    �� TextWrapping$
 Wrap=�6j   $#��Press 'GO' to receive asserts��6~   $	��Top=�6�   $	��315��6�   �� 5   
   -   5         ��Name$ convertButton��5      $	��GO!��6(   $
��Left=�66   $��57��6Q   $��367,196,0,0q�6]   $	��Top=�6r   $��55��6�   6�   �� 5   
   -   5         ��Name$ getQueryRichTextBox��5      $
��Left=�64   $	��612��6O   $��	52,71,0,0q�6\   $	��Top=�6o   $	��310��6�   . �5      /� .T�5      J� .*�6   .*�5      �� $�Paste your query here...��6   5      5      5      �� 5   
   -   5        $��	ComboBox1��5      $	��175��6$   $��23��60   $	��Top=�5      $
��Left=�6#   $��
567,25,0,0q�5       J� RenderTransformOrigin$ 	0.5,0.565&�6!   .@�5      �� $��oDtb(index)��6    �� 
IsSelected . 6F   �� 5      $ ��oDtb.Rows(0).ItemArray(1))��6   $��0,0,-2,0q�6@   6T   5      �� 5   
   $��Select type of assertion:��6   $
��Left=�64   $��
427,26,0,0q�6O   $	��Top=�6c   5      5      