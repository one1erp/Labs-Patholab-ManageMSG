using Patholab_DAL_V1;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataLayer dal = new DataLayer();
            dal.MockConnect();
            //Get all clinics and convert them to a list
            var clinics = dal.GetAll<U_CLINIC>().ToList();
            comboBoxClinics.DisplayMemberPath = "NAME";// clinics;
            //Set the itemSource to show the clinics names
            comboBoxClinics.ItemsSource = clinics;
            //Add event listener when the clinic is changed
            comboBoxClinics.SelectionChanged += new SelectionChangedEventHandler(ComboBoxClinics_SelectedIndexChanged);

            //Get 1000 doctors and convert them to a list
            var doctors = dal.GetAll<SUPPLIER>().Take(1000).ToList();
            comboBoxRefferDR.DisplayMemberPath = "SUPPLIER_USER.U_LAST_NAME";
            ;
            //Set the itemSource to show the doctors names
            comboBoxRefferDR.ItemsSource = doctors;
            comboBoxRefferDR.SelectionChanged += new SelectionChangedEventHandler(ComboBoxRefferDR_SelectedIndexChanged);
            comboBoxImpDR.DisplayMemberPath = "SUPPLIER_USER.U_LAST_NAME";
            //Set the itemSource to show the doctors names
            comboBoxImpDR.ItemsSource = doctors;
            comboBoxImpDR.SelectionChanged += new SelectionChangedEventHandler(ComboBoxImpDR_SelectedIndexChanged);

            //Get all the customers and conver them to a list
            var customers = dal.GetAll<U_CUSTOMER>().ToList();
            comboBoxCustomer.DisplayMemberPath = "NAME";
            comboBoxCustomer.ItemsSource = customers;
            comboBoxCustomer.SelectionChanged += new SelectionChangedEventHandler(ComboBoxCustomer_SelectedIndexChanged);

            //Get all the customers and conver them to a list
            var clients = dal.GetAll<CLIENT_USER>().Take(1000).ToList();
            List<String> fullNames = new List<string>();
            foreach (CLIENT_USER client in clients)
            {
                fullNames.Add(client.U_FIRST_NAME + " " + client.U_LAST_NAME);
            }
            //comboBoxClient.DisplayMemberPath = "U_FIRST_NAME";
            comboBoxClient.ItemsSource = fullNames;
            comboBoxClient.SelectionChanged += new SelectionChangedEventHandler(ComboBoxClient_SelectedIndexChanged);
        }

        private void Find_Button_Click(object sender, RoutedEventArgs e)
        {
            DataLayer dal = new DataLayer();
            dal.MockConnect();

            if (userTextBoxID.Text != "")
            {
                int userID = Int32.Parse(userTextBoxID.Text);
                U_SAMPLE_MSG_USER SMU = dal.FindBy<U_SAMPLE_MSG_USER>(x => x.U_SAMPLE_MSG_ID == userID).FirstOrDefault();
                //fk--SMU.U_ORDER.U_ORDER_USER.U_CUSTOMER
                //navigation SMU.U_ORDER.U_ORDER_USER.U_CUSTOMER1
                if (SMU != null)
                {
                    //Updating these fields to the proper user's data
                    if (SMU.U_PATIENT_FIRST_NAME != "")
                    {
                        firstNameRaw.Content = SMU.U_PATIENT_FIRST_NAME;
                    }
                    else
                    {
                        firstNameRaw.Content = "Missing";
                    }
                    if (SMU.CLIENT != null && SMU.CLIENT.CLIENT_USER.U_FIRST_NAME != "")
                    {
                        firstNameCal.Content = SMU.CLIENT.CLIENT_USER.U_FIRST_NAME;
                    }
                    else
                    {
                        firstNameCal.Content = "Missing";
                    }
                    if (SMU.U_PATIENT_LAST_NAME != "")
                    {
                        lastNameRaw.Content = SMU.U_PATIENT_LAST_NAME;
                    }
                    else
                    {
                        lastNameRaw.Content = "Missing";
                    }
                    if (SMU.CLIENT != null && SMU.CLIENT.CLIENT_USER.U_LAST_NAME != "")
                    {
                        lastNameCal.Content = SMU.CLIENT.CLIENT_USER.U_LAST_NAME;
                    }
                    else
                    {
                        lastNameCal.Content = "Missing";
                    }

                    if (SMU.U_PATIENT_GENDER != "")
                    {
                        genderRaw.Content = SMU.U_PATIENT_GENDER;
                    }
                    else
                    {
                        genderRaw.Content = "Missing";
                    }
                    if (SMU.CLIENT != null && SMU.CLIENT.CLIENT_USER.U_GENDER != "")
                    {
                        genderCal.Content = SMU.CLIENT.CLIENT_USER.U_GENDER;
                    }
                    else
                    {
                        genderCal.Content = "Missing";
                    }

                    if (SMU.U_PATIENT_ID_TYPE != "")
                    {
                        IDTypeRaw.Content = SMU.U_PATIENT_ID_TYPE;
                    }
                    else
                    {
                        IDTypeRaw.Content = "Missing";
                    }
                    if (SMU.CLIENT != null && SMU.CLIENT.CLIENT_USER.U_ID_CODE != "")
                    {
                        IDTypeCal.Content = SMU.CLIENT.CLIENT_USER.U_ID_CODE;
                    }
                    else
                    {
                        IDTypeCal.Content = "Missing";
                    }
                    U_CLINIC clinic = dal.FindBy<U_CLINIC>(x => x.U_CLINIC_ID == SMU.U_CLINIC_ID).FirstOrDefault();
                    if (SMU.U_CLIENT_ID != null)
                    {

                        clinicRaw.Content = clinic.NAME;
                    }
                    else
                    {

                        clinicRaw.Content = "Missing";
                    }
                    clinic = dal.FindBy<U_CLINIC>(x => x.U_CLINIC_ID == SMU.U_CLINIC.U_CLINIC_ID).FirstOrDefault();
                    if (clinic.NAME != "")
                    {

                        clinicCal.Content = clinic.NAME;
                    }
                    else
                    {
                        clinicCal.Content = "Missing";
                    }

                    if (SMU.U_IMP_PHYSICIAN_NAME != "")
                    {
                        implementingDoctorRaw.Content = SMU.U_IMP_PHYSICIAN_NAME;
                    }
                    else
                    {
                        implementingDoctorRaw.Content = "Missing";
                    }
                    if (SMU.SUPPLIER != null)
                    {
                        implementingDoctorCal.Content = SMU.SUPPLIER.SUPPLIER_USER.U_LAST_NAME;
                    }
                    else
                    {
                        implementingDoctorCal.Content = "Missing";
                    }
                    if (SMU.U_REF_PHYSICIAN_NAME != "")
                    {
                        refferingDoctorRaw.Content = SMU.U_REF_PHYSICIAN_NAME;
                    }
                    else
                    {
                        refferingDoctorRaw.Content = "Missing";
                    }
                    if (SMU.SUPPLIER1 != null)
                    {
                        refferingDoctorCal.Content = SMU.SUPPLIER1.SUPPLIER_USER.U_LAST_NAME;
                    }
                    else
                    {
                        refferingDoctorCal.Content = "Missing";
                    }
                    string status = ShowStatus(SMU.U_STATUS);
                    statusPhrase.Content = status;

                    if (SMU.U_INSTNAME != "")
                    {
                        customerRaw.Content = SMU.U_INSTNAME;
                    }
                    else
                    {
                        customerRaw.Content = "Missing";
                    }
                }
            }

        }

        private string ShowStatus(string status)
        {
            if (status == "N")
            {
                return "מסר חדש";
            }
            if (status == "H")
            {
                return "בדיקת נתוני מסר ע\"י מנהל";
            }
            if (status == "I")
            {
                return "נשלח למכשיר הקלט";
            }
            if (status == "A")
            {
                return "נוצרה דרישה";
            }
            if (status == "P")
            {
                return "דרישה עם נתונים חסרים";
            }
            if (status == "R")
            {
                return "דרישה מוכנה לעבודה";
            }
            if (status == "X")
            {
                return "מבוטל";
            }
            if (status == "S")
            {
                return "מסר עדכון/ביטול";
            }
            return "העדכון בוצע";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataLayer dal = new DataLayer();
            dal.MockConnect();

            int userID = Int32.Parse(userTextBoxID.Text);
            U_SAMPLE_MSG_USER SMU = dal.FindBy<U_SAMPLE_MSG_USER>(x => x.U_SAMPLE_MSG_ID == userID).FirstOrDefault();

            //Get the selected item from the comboBoxRefferDR
            SUPPLIER selectedItem = (SUPPLIER)comboBoxRefferDR.SelectedItem;
            if (selectedItem != null)
            {
                SMU.SUPPLIER1 = dal.FindBy<SUPPLIER>(x => x.SUPPLIER_ID == selectedItem.SUPPLIER_ID).FirstOrDefault();
                ;
            }

            //Get the selected item from the comboImpDR
            selectedItem = (SUPPLIER)comboBoxImpDR.SelectedItem;
            if (selectedItem != null)
            {
                SMU.SUPPLIER = dal.FindBy<SUPPLIER>(x => x.SUPPLIER_ID == selectedItem.SUPPLIER_ID).FirstOrDefault();
            }

            //Get the selected item from comboClinic
            U_CLINIC clinic = (U_CLINIC)comboBoxClinics.SelectedItem;
            long? clinicID = SMU.U_CLINIC_ID;
            if (clinic != null)
            {

                SMU.U_CLINIC = dal.FindBy<U_CLINIC>(x => x.U_CLINIC_ID == clinic.U_CLINIC_ID).FirstOrDefault();

            }

            //Get the selected item from comboCustomer
            U_CUSTOMER customer = (U_CUSTOMER)comboBoxCustomer.SelectedItem;
            if (customer != null)
            {
                SMU.U_INSTNAME = customer.NAME;
            }

            //Get the selected item from comboClients
            CLIENT_USER client = findClientUserFromComboBox((String)comboBoxClient.SelectedItem);
            if (client != null)
            {
                SMU.CLIENT = dal.FindBy<CLIENT>(x => x.CLIENT_USER.CLIENT_ID == client.CLIENT_ID).FirstOrDefault();

            }

            dal.SaveChanges();

        }

        private void ComboBoxClinics_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //Get the sender comboBox details
            ComboBox comboBox = (ComboBox)sender;
            //Get the selected item from the comboBox
            U_CLINIC selectedItem = (U_CLINIC)comboBox.SelectedItem;
            //Set the selected item to be the new clinic of the client
            clinicCal.Content = selectedItem.NAME;
        }

        private void ComboBoxRefferDR_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //Get the sender comboBox details
            ComboBox comboBox = (ComboBox)sender;
            //Get the selected item from the comboBox
            SUPPLIER selectedItem = (SUPPLIER)comboBox.SelectedItem;
            refferingDoctorCal.Content = selectedItem.SUPPLIER_USER.U_LAST_NAME;
        }

        private void ComboBoxImpDR_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //Get the sender comboBox details
            ComboBox comboBox = (ComboBox)sender;
            //Get the selected item from the comboBox
            SUPPLIER selectedItem = (SUPPLIER)comboBox.SelectedItem;
            implementingDoctorCal.Content = selectedItem.SUPPLIER_USER.U_LAST_NAME;
        }

        public void ComboBoxCustomer_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            U_CUSTOMER selectedItem = (U_CUSTOMER)comboBox.SelectedItem;
            customerRaw.Content = selectedItem.NAME;
        }

        public void ComboBoxClient_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            CLIENT_USER selectedItem = findClientUserFromComboBox((String)comboBox.SelectedItem);
            firstNameCal.Content = selectedItem.U_FIRST_NAME;
            lastNameCal.Content = selectedItem.U_LAST_NAME;
        }
        public CLIENT_USER findClientUserFromComboBox(String fullName)
        {
            DataLayer dal = new DataLayer();
            dal.MockConnect();
            String[] nameSplit = fullName.Split(' ');
            String firstName = nameSplit[0], lastName = nameSplit[1];
            CLIENT_USER selectedItem = dal.FindBy<CLIENT_USER>(x => x.CLIENT.CLIENT_USER.U_FIRST_NAME == firstName && x.CLIENT.CLIENT_USER.U_LAST_NAME == lastName).FirstOrDefault();
            return selectedItem;
        }
    }
}
