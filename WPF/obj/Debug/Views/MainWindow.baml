   M S B A M L   `   `   `  ����>  :WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WPFWPF  5   	   G(clr-namespace:SystemOpMobComm.ViewModelsSystemOpMobComm.ViewModels  5   	       WPF.Views.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   LmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes     5   	   #localclr-namespace:WPF     5   	   ?
viewmodels(clr-namespace:SystemOpMobComm.ViewModels     5   	     � WindowStartupLocation$  CenterScreen=�5   	   $	��728��5   	   $	��500��6    � Icon$$ /WPF;component/Images/ICON.png��5   	    � Title$ 
LOGIN FORM��5   	   $	��500��6   $	��728��6,   .��5      � 
������� � RowDefinitions 5   
   �� 5      $��2.00* �6   6.   �� 5      $��0.75* �6   6-   �� 5      $��0.75* �6   6-   �� 5      $��1.75* �6   6-    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   .+�5   
   �� $��1��6   $��0��6$   $��3��61   $��Авторизация��6E   $��Bottom=�6X   $��Center=�6s   $��24(�6�   $%��Bahnschrift Light SemiCondensed)�6�    J� RenderTransformOrigin$ 0.522,-0.119&�6�   $��0,0,0,25q�6�   �� 5"   
   $��2��6   $��0��6    $%��/WPF;component/Images/login.png��6-   $	��Top=�6V   $��Center=�6n   $��70��6�   $��70��6�   $��0,71,0,0q�6�   $ 
0.41,1.819&�6�   �� 5$   
   $��1��6   $��1��6%   $��3��62    �� Orientation$ Vertical=�6F   $��Center=�6]   $
��16,0q�6x   .+�5%      �� $ 
Horizontal=�6   .+�5&      �� $ ��Введите логин:��6   $��20(�62   $
��Left=�6@   $��Center=�6[   $%��Bahnschrift Light SemiCondensed)�6v   5'      �� 5(      $��20(�6   $��2,8,0,0q�6`   $
��Left=�6q   $	��255��6�    �� 
CaretBrush �	#FF49695A �� SelectionBrush �	#FF49695A��6$   �� *Login+	 �� UpdateSourceTrigger$	 PropertyChanged=�6�   5)      �� 5*   
   $��1��6   $��2��6%   $��3��62   $ Vertical=�6F   $��Center=�6]   $
��16,0q�6x   .+�5+      �� $ 
Horizontal=�6   .+�5,      �� $"��Введите пароль:��6   $��20(�63   $
��Left=�6A   $��Center=�6\   $%��Bahnschrift Light SemiCondensed)�6w   5-      G� 5.      -   5        $��Password��5.   (   $��20(�6   $��
0,8,-0.4,0q�69   $	��255��6M   $
��Left=�6Y   $��Center=�6t   
 G� 
CaretBrush
 �	#FF49695A G� SelectionBrush �	#FF49695A6�   5/      �� 50   
   $��1��6   $��3��6%   $��3��62   .+�51      �� $��Center=�6a   $	��Top=�6~   $��
Войти��6�   $	��200��6�   $��30��6�   $��22(�6�   $��0,10,0,0q�6�   $%��Bahnschrift Light SemiCondensed)�6�   ��6   �� *Enter+��6/   ��  �� ElementName$ Password��6�   �� 52      $��22(�6   
���  ��$��Center=�67   $��Center=�6T   $��0,4,0,0q�6o   ��53      �� 54       �� 
TargetType" ��6    �� Triggers 55      �� 56       �� Binding 6*   �� *Wrong+ �� Value 06D   .a�57   "   ��  �� Property � y� �� Value$ 	Collapsed=�6?   58      �� 59       6*   �� *Wrong+ 16D   .a�5:   "   ��  � ��$K EНе найден пользователь с этим логином��69   5;      �� 5<       6*   �� *Wrong+ 26D   .a�5=   "   ��  � ��$. (Пароль введен неверно��69   5>      5@      5B      5C      5E      5F      