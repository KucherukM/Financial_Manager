< Window x: Class = "FinanceManager.MainWindow"
       xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation"
       xmlns: x = "http://schemas.microsoft.com/winfx/2006/xaml"
       Title = "Finance Manager" Height = "400" Width = "300" Background = "DodgerBlue" >
< Grid >
< !--���������-- >
< TextBlock Text = "Manager finance"
                  FontSize = "28"
                  FontWeight = "Bold"
                  HorizontalAlignment = "Center"
                  VerticalAlignment = "Top"
                  Margin = "0,20,0,0" />
< !--ϳ���������� ��� ����� -->
<TextBlock Text="Login"
                  FontSize="22"
                  HorizontalAlignment="Center"
                  VerticalAlignment="Top"
                  Margin="0,70,0,0" />
<!-- ���� ��� �������� ���� ����������� ��� ���������� ����� -->
<TextBox PlaceholderText="Username or Email"
                Margin="40,140,40,0"
                VerticalAlignment="Top"
                Height="30"
                FontSize="14" />
<!-- ���� ��� �������� ������ -->
<PasswordBox Margin="40,180,40,0"
                    VerticalAlignment="Top"
                    Height="30" />
<!-- ������ ����� -->
<Button Content="Button login"
               HorizontalAlignment="Center"
               VerticalAlignment="Top"
               Margin="0,230,0,0"
               Width="100"
               Height="30" />
<!-- ����� � ������ ��������� -->
<StackPanel Orientation="Horizontal"
                   HorizontalAlignment="Center"
                   VerticalAlignment="Bottom"
                   Margin="0,0,0,30">
<TextBlock Text="Not a member yet?"
                      FontSize="14"
                      VerticalAlignment="Center" />
<Button Content="Register!"
                   FontSize="14"
                   Foreground="Blue"
                   Background="Transparent"
                   BorderBrush="Transparent"
                   VerticalAlignment="Center"
                   Padding="0"
                   Cursor="Hand"
                   Click="RegisterButton_Click" />
</StackPanel>
</Grid>
</Window>