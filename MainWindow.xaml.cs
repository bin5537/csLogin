using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// Mysql 커넥터
using MySqlConnector;

namespace csLogin
{
    public partial class MainWindow : Window
    {
               // Mysql 연결 (문자열 이용)
        private string connectSql = "server=localhost;database=prj_2025;user=root;password=비밀번호";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                    // 유저 입력값을 불러와 Trim을 이용하여 LoginCheck 함수 실행
            if (LoginCheck(UserIdBox.Text.Trim(), UserPasswordBox.Text.Trim()))
            {
                MessageBox.Show("로그인 성공");
            }
            else
            {
                MessageBox.Show("로그인 실패");
            }
        }

               // LoginCheck 함수
        private bool LoginCheck(string userId, string userPassword)
        {
            
            using (MySqlConnection connect = new MySqlConnection(connectSql))   // Mysql 연결 객체
            {
                try // 연결 성공시
                {   
                    connect.Open();     // 데이터베이스 연결

                                   // 쿼리문을 이용해 cs_users를 선택하여 user_id와 password 찾기
                    string query = $"SELECT EXISTS(SELECT 1 FROM cs_users WHERE user_id='{userId}' AND password='{userPassword}')";

                    MySqlCommand command = new MySqlCommand(query, connect);

                                    // 결과 값을 return
                    return Convert.ToBoolean(command.ExecuteScalar());
                }
                catch
                {
                                    // 실패시 false
                    return false;
                }
            }
        }

        private void UserIdBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UserPasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
