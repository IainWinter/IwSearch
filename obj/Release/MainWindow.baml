   M S B A M L   `   `   `  ���� C  ?IwSearch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:IwSearchIwSearch      IwSearch.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     (localclr-namespace:IwSearch     -     � Title$  Search��$	��600��$
��1125��$	��600��$
��1125��.��� .+��� -    ��Name$ 
tabControl��$��Stretch=�$��Stretch=�$��0,2,0,0q�.@��� -    ��Name$ options_tab��$��Options��.��� ���	#FFE5E5E5$��0,1,0,-1q�.+��� -    ��Name$ label��$��Searching in files��$
��Left=�$��5,7,0,0q�$	��Top=��� -    ��Name$$ searchOnlyInTextFiles_checkBox��$��Search only in text files��$
��Left=�$��	10,30,0,0q�$	��Top=����	#FFACACAC�� -    ��Name$ searchType_comboBox��$
��Left=�$��	11,48,0,0q�$	��Top=�$	��127������  �� EndPoint$ 0.5,1&� � MappingMode$ RelativeToBoundingBox=� �� 
StartPoint$ 0.5,0&�.��� 	 � Color$	 	#FFF0F0F0��
 � Offset$
 0Q�� $	 	#FFE5E5E5��$
 1Q�� $	 	#FFEFEFEF��.@��� $��Require Line in file���� $��Line can be in file���� $��Don't search in file���� -   $' !returnOnFirstInstanceOfLine_label��$��Searching in files type��$
��Left=�$��
137,46,0,0q�$	��Top=��� -   $* $returnOnFirstInstanceOfLine_checkBox��$(��"Find all instances of line in file��$
��Left=�$��	10,73,0,0q�$	��Top=����	#FFACACAC�� -	   $ searchLargeFiles_checkBox��$��Search large files to end��$
��Left=�$��	10,90,0,0q�$	��Top=����	#FFACACAC�� -
   $ 
label_Copy��$��Other Options��$
��Left=�$��	5,101,0,0q�$	��Top=��� -    ��Name$ enableSearching_Button��$$��Enable searching from Explorer��$
��Left=�$��
10,125,0,0q�$	��Top=�$	��185��$��22���� -   $ removeSearching_Button��$%��Disable searching from Explorer��$
��Left=�$��
10,155,0,0q�$	��Top=�$	��185��$��22���� -   $ 
tab_Search��$��Search��.��� ���	#FFE5E5E5.+��� -    ��Name$ path_TextBox��$��23��$��100,7,110,0q�$	��Top=��� -   $ browse_button��$��Browse��$��Right=�$��0,7,7,0q�$	��Top=�$��98��$��23���� -   $ name_TextBox��$
��Left=�$��23��$	��Top=�$	��239��$��
100,38,0,0q� �� 	MaxLength$	 255���� -   $ inFile_TextBox��$
��Left=�$��23��$	��Top=�$	��239��$��
100,72,0,0q��� -   $ type_TextBox��$
��Left=�$��23��$	��Top=�$	��239��$��100,104,0,0q��� -   $ minsize_TextBox��$��22��$��100,137,0,0q�$	��Top=�$	��117��$
��Left=��� -   $ minsize_ComboBox��$��222,137,0,0q�$	��Top=�$	��117��$��22��$
��Left=�.@��� $��Byte(s)���� $��Kilobyte(s)���� $��Megabyte(s)���� $��Gigabyte(s)���� $��Terabyte(s)���� -   $ maxsize_TextBox��$��22��$��100,169,0,0q�$	��Top=�$	��117��$
��Left=��� -   $ maxsize_ComboBox��$��222,169,0,0q�$	��Top=�$	��117��$��22��$
��Left=�.@��� $��Byte(s)���� $��Kilobyte(s)���� $��Megabyte(s)���� $��Gigabyte(s)���� $��Terabyte(s)��(  "System.Windows.Controls.DatePicker  -     Name$ minDateMod_date��$
��Left=�$��100,201,0,0q�$	��Top=��� -   $
 dash��$��-��$
��Left=�$	��Top=�$��212,199,0,0q�$��27��  -   $ maxDateMod_date��$
��Left=�$��237,201,0,0q�$	��Top=�  -   $ minDateCre_date��$
��Left=�$��100,235,0,0q�$	��Top=��� -   $ dash2��$��-��$
��Left=�$	��Top=�$��212,233,0,0q�$��27��  -   $ maxDateCre_date��$
��Left=�$��237,235,0,0q�$	��Top=��� -   $ startSearch_Button��$��Start Search��$
��Left=�$��Bottom=�$	��330��$��7,0,0,35q�$��23���� -   $ stopSearch_Button��$��Stop Search��$
��Left=�$��Bottom=�$	��330��$��7,0,0,7q�$��23��� $��	344,0,0,0q� � RowDefinitions �� $	��68* �$	��200���� $��4 ��� $	��39* �$	��200��.+�&   System.Windows.Controls.DataGrid  -     Name$ results_dataGrid��$��Stretch=�   HeadersVisibility$ Column=����	#FF787878   AutoGenerateColumns .     SelectionMode$ Single=�   CanUserAddRows .     CanUserSortColumns .    CanUserReorderColumns .     GridLinesVisibility$
 None=�
�������$��0,38,7,0q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 3*    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *name+5  /System.Windows.Controls.DataGridHyperlinkColumn  $
 Path��$ 3*  .  �� *path+  $
 Type��$ *  .  �� *type+  $
 Size��$ *  .  �� *size+  $ Date Modified��$ *  .  �� *dateModified+  $ Date Created��$ *  .  �� *dateCreated+  $
 Line��$ *  .  �� *	lineIndex+ � 	Resources  �� Color �� Opacity*  $System.Windows.Controls.DataGridCell  �� 
TargetType! �� Property" �� Value��%�   (��       * SystemColors.HighlightBrushKey+)'       '�� ;     �� $ Black��$ 1Q��� "   .��� ! � ��$" 0q��� "  ��.�-    �� $��Stretch=�$��1��$��0,0,7,0q����	#FF999999�� -!   # ��Name$# fileRead_textBox��$��2��"$ �� VerticalScrollBarVisibility$
$ Auto=�$��0,0,7,7q����	#FF787878|�.  % J� IsHitTestVisible% . & J� 	AllowDrop& .  $��Arial)�. �/� �� -"   $ 
path_Label��$
��Path��$
��Left=�$	��Top=�$��5,7,0,0q�$��23���� -#   $ 
name_Label��$
��Name��$
��Left=�$	��Top=�$��5,35,0,0q�$��23���� -$   $ name_Label_Copy��$��Line In File��$
��Left=�$	��Top=�$��5,69,0,0q�$��23��$��90���� -%   $ 
type_Label��$
��Type��$
��Left=�$	��Top=�$��	7,102,0,0q�$��26���� -&   $ minsize_Label��$��Min Size��$
��Left=�$	��Top=�$��	5,136,0,0q�$��24��$��65���� -'   $ maxssize_Label��$��Max Size��$
��Left=�$	��Top=�$��	5,168,0,0q�$��24��$��65���� -(   $ 	mod_Label��$��Date Modified��$
��Left=�$	��Top=�$��	4,201,0,0q�$��23���� -)   $ 	cre_Label��$��Date Created��$
��Left=�$	��Top=�$��	4,235,0,0q�$��23���� $
��Left=�$��8��$��
10,265,0,0q�$	��Top=�$	��329���� -*   $ filesSearched_label��$��Files searched:��$
��Left=�$��	4,303,0,0q�$	��Top=�$��86���� -+   $  filescontentsearched_label��$��Files content searched:��$
��Left=�$��	4,320,0,0q�$	��Top=�$	��135���� -,   $ elapsedTime_label��$��Elapsed Time:��$
��Left=�$��	4,269,0,0q�$	��Top=�$��86���� --   $ resultsFound_label��$��Results found:��$
��Left=�$��	4,337,0,0q�$	��Top=�$	��135���� -.   $ threadsSearching_label��$��Threads searching:��$
��Left=�$��	4,286,0,0q�$	��Top=�$	��135���� -/   ' ��Name$' elapsedTime_textBlock��$
��Left=�$��139,274,0,0q�$
��Wrap=�$	��Top=�$	��198���� -0   $' filesSearched_textBlock��$
��Left=�$��139,308,0,0q�$
��Wrap=�$	��Top=�$	��198���� -1   $$' filescontentsearched_textBlock��$
��Left=�$��139,325,0,0q�$
��Wrap=�$	��Top=�$	��198���� -2   $' resultsRound_textBlock��$
��Left=�$��139,342,0,0q�$
��Wrap=�$	��Top=�$	��198���� -3   $ ' threadsSearching_textBlock��$
��Left=�$��139,291,0,0q�$
��Wrap=�$	��Top=�$	��198��