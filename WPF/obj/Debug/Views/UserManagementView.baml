   M S B A M L   `   `   `  ����  WPF"    WPF.Views.UserManagementView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Icon$$  /WPF;component/Images/ICON.png��5   	    � Title$ USER MANAGMENT��5   	   $	��450��6    $	��800��6-   .��5      � .+�5   
   �� ��6   �� *Users+ �� SelectedItem 61   �� *SelectedUser+ �� View 5      �� 5	      .P�5
      �� $��
Номер��6%    �� DisplayMemberBinding 64   �� *PhoneNumber+�� 5      $��ФИО��6%    62   �� *Name+�� 5      $��Баланс��6%    65   �� *	Balance+�� 5      $��
Тариф��6%    64   �� *
TariffName+5      5      �� 5   
   $��Bottom=�6    �� Orientation$ 
Horizontal=�60   $��Center=�6I   .+�5      �� $��Подтвердить��6   $��10q�6U   $	��120��6a   $��35��6m   ���#007BFF��6+   �� *ConfirmChangesCommand+6y   �� 5      $��Добавить��6   $��10q�6K   $	��100��6W   $��35��6c   ��6(   �� *AddUserCommand+6c   �� 5      $��Удалить��6   $��10q�6M   $	��100��6Y   $��35��6e   ��6'   �� *DeleteUserCommand+6e   �� 5      $��
Выход��6   $��10q�6E   $	��100��6Q   $��35��6]   ��6%   �� *ExitCommand+6]   5      �� 5   
   $��Bottom=�6   $ Vertical=�60   $��Right=�6G   $��
0,0,20,150q�6c   .+�5      �� $��Пользователь��6   $��16(�6,   $��	0,0,10,10q�6:   �� 5      $��ФИО��6   �� 5      ��6   �� *SelectedUser.Name+ �� UpdateSourceTrigger$ PropertyChanged=��� 5      $��
Номер��6   �� 5       �� 
IsReadOnly . ��6   �� *SelectedUser.PhoneNumber+6@   �� 5      $��Баланс��6   �� 5      ��6   �� *SelectedUser.Balance+$ PropertyChanged=��� 5      $��
Тариф��6   �� 5       ��6   �� *SelectedUser.TariffName+$ PropertyChanged=�5!      5"      5#      