   M S B A M L   `   `   `  ����B  >WPFView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WPFViewWPFView  5   	       WPFView.UpdateWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WPFView     5   	     � Title$  UpdateWindow��5   	   $	��242��6   $	��494��6+   .��5	      A� .+�5
   
   �� $'��!Изменить студента��6   .��5      �  � ColumnDefinitions 5      �� 5      $
��113* �6'   �� 5      $
��336* �6'    � RowDefinitions 5      �� 5      $	��39* �6$   �� 5      $	��32* �6$   �� 5      $	��32* �6$   �� 5      $	��58* �6$   .+�5      �� $��ФИО��6   $
��Left=�6&   $��	10,10,0,0q�6A   $	��Top=�6T    J� RenderTransformOrigin$ 
0.451,0.34&�6l   �� 5      $��Группа��6   $
��Left=�6)   $��10,2,0,0q�6D   $	��Top=�6V   $��1��6n   �� 5      $ ��Специальность��6   $
��Left=�60   $	��Top=�6K   $��10,5,0,0q�6c   $��2��6u   �� 5      -   5        $
��Left=�5   W   $��7,0,0,0q�6r    �� TextWrapping$
 Wrap=�6�   $��Center=�6�   $	��306��6�   $��1��6�    � DataContext 6   �� *CurrentStudent+��6A   �� *Name+6�   �� 5      -   5        $
��Left=�5   X   $��10,8,0,0q�6s   $
 Wrap=�6�   $	��Top=�6�   $	��306��6�   $��1��6�   $��1��6�    6   �� *CurrentStudent+��6A   �� *Group+6�   �� 5      $��1��6   $��Изменить��6)   $
��Left=�6�   $��7,0,0,0q�6�   $��Center=�6�   $��36��6�   $	��306��6	  $��3��6  ��6<   �� *UpdateStudentCommand+��6e   ��  �� RelativeSource ��  �� AncestorType$ WindowL�6  �� 5       �� SelectedValuePath$ Content��6B   $
��Left=�6�   $��7,0,0,0q�6�   $��Center=�6�   $	��306��6�   $��1��6�   $��2��6�    6   �� *CurrentStudent+	 �� SelectedValue	 6^   �� *
Speciality+.@�5      �� $��ИСИТ��6#   �� 5      $
��ИБ��6#   �� 5      $��ИВТ��6#   �� 5       $
��ПИ��6#   5!      5"      5#      5$      5%      