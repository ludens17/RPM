using RPM.app;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace RPM
{
    /// <summary>
    /// Логика взаимодействия для EVG.xaml
    /// </summary>


    public partial class EVG : Page
    {
        //private Agent _currentAgens = new Agent();
        public EVG()
        {
            InitializeComponent();
           // var currentag = agensEntities.GetContext().Agent.ToList();
            var currentag = connect.conObj.Agent.ToList();
            //lst.ItemsSource = currentag;


            /*  DataContext = _currentAgens;
              if (_currentAgens.Logo == null)
              {
                  _currentAgens.Logo.
              }
              */
            try
            {
                //List<Agent> list = agensEntities.GetContext().Agent.ToList();
                SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=agens;Integrated Security=True");
                foreach (Agent item in currentag)
                {
                    SqlCommand sqlcmd = new SqlCommand($"(SELECT SUM(ProductCount) FROM dbo.ProductSale WHERE AgentID = {item.ID} AND YEAR(dbo.ProductSale.SaleDate) = {Convert.ToInt32(DateTime.Now.Year)} )", connection);
                    connection.Open();
                    item.PtdSale = sqlcmd.ExecuteScalar().ToString();
                    connection.Close();
                }
                foreach (Agent item in currentag)
                {
                    SqlCommand sqlcmd = new SqlCommand($"(SELECT Logo FROM dbo.Agent)", connection);
                    connection.Open();
                    item.imgpath = sqlcmd.ExecuteScalar().ToString();
                    connection.Close();
                }
                //calculate discount 
                try
                {
                    foreach (Agent y in currentag)
                    {

                        SqlCommand sqlcmd = new SqlCommand($"(SELECT SUM(ProductCount) FROM dbo.ProductSale WHERE AgentID = {y.ID})", connection);
                        connection.Open();
                        int i = Convert.ToInt32(sqlcmd.ExecuteScalar().ToString());
                        connection.Close();
                        y.skidka = i > 500000 ? "25" : i > 149999 ? "20" : i > 49999 ? "10" : i > 4999 ? "5" : i > 9999 ? "0" : "0";




                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Не у всех агентов присутствуют актуальные записи продаж в бд", "Предупреждение", MessageBoxButton.OK);
                }
                lst.ItemsSource = currentag;
            }
            catch (Exception)
            {
                MessageBox.Show("Отсутствует доступ к БД", "Ошибка", MessageBoxButton.OK);
            }

        }
    }

    
}

