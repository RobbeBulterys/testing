   M S B A M L   `   `   `  ����>  :GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GUIGUI  5   	       GUI.MainWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:GUI     5   	     � Title$  Login Screen��5   	   $	��350��6   $	��400��6+   .��5	      ]� ��5
   
   �� 5       �� ImageSource$# Pictures/Login-Background.jpg��6    j� Stretch$ UniformToFill=�6E   .+�5   
   �� $��Center=�6   $	��320��60   $��10q�6<   .+�5      �� $��	Loging In��6   $��Center=�6(   $
��Bold"�6E   $��30(�6W   �� 5      $��	UserName:��6   $��20(�6(   $��Center=�66   �� 5      -   5         ��Name$ txtUserName��5      $��Center=�6+   $	��300��6H   $��30��6T   $��Center=�6`   �� 5      $��	Password:��6   $��20(�6(   $��Center=�66   ]� 5      $	��300��6   $��Center=�6$   $��30��6A   .+�5      G� -   5         G�Name$ txtPassWord��5      $	��250��63   $��Center=�6?   �� 5      -   5         ��Name$ btnLogin��5      
�������$��>��6B   $
��Bold"�6Q   $��20(�6c   $��50��6q      PrimaryButtonStyle#���   6|   6�   5      ]� 5      .+�5      �� -   5        $ btnCreateAccount��5      #���   63   
������ 
�������$
��Bold"�6�   $��0q�6�   $	��125��6�   $��Create new account��6�   $
��Left=�6�   $��
0,10,10,10q�6�   6
  �� 5      -   5         ��Name$ lblErrorMessage��5      
���  ��
������ $��Center=�6[   5      5      5      5      