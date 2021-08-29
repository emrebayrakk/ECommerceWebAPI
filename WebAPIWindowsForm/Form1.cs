
using Entities.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebAPIWindowsForm
{
    public partial class Form1 : Form
    {
        #region Define
        private string url = "http://localhost:28493/api/";
        private int selectedId = 0;
        #endregion

        #region Form1

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            await DataGridViewFill();
            cmbGenderFill();

        }

        #endregion





        #region Methods
        private async Task DataGridViewFill()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var users = await httpClient.GetFromJsonAsync<List<UserDetailDto>>(url + "Users/GetList");
                dataGridView1.DataSource = users;
            }
        }

        void cmbGenderFill()
        {
            List<Gender> genders = new List<Gender>();
            genders.Add(new Gender(){Id=1,GenderName = "Erkek"});
            genders.Add(new Gender(){Id=2,GenderName = "Kadın"});
            comboBox1.DataSource = genders;
            comboBox1.DisplayMember = "GenderName";
            comboBox1.ValueMember = "Id";
        }

        class Gender
        {
            public int Id { get; set; }
            public string GenderName { get; set; }
        }
        void ClearForm()
        {
            txtFirstName.Text =String.Empty;
            txtLastName.Text=String.Empty;
            txtAddress.Text=String.Empty;
            txtEmail.Text=String.Empty;
            txtPassword.Text=String.Empty;
            comboBox1.SelectedValue = 0;
            txtUserName.Text=String.Empty;
            dateTimePicker1.Value=DateTime.Now;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
        #endregion






        #region Crud

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                UserAddDto userAddDto = new UserAddDto()
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Address = txtAddress.Text,
                    DateOfBirth = Convert.ToDateTime(dateTimePicker1.Text),
                    Email = txtEmail.Text,
                    Gender = comboBox1.Text=="Erkek"?true : false,
                    Password = txtPassword.Text,
                    UserName = txtUserName.Text,
                };
                HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(url+"Users/Add" , userAddDto);
                if (responseMessage.IsSuccessStatusCode)
                {
                    await DataGridViewFill();
                    MessageBox.Show("Ekleme İşlemi Başarılı");
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Başarısız Ekleme");
                }
            }
        }

        private void dtpDateOfBirth_ValueChanged(object sender, EventArgs e)
        {

        }

        private async void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            using (HttpClient httpClient = new HttpClient())
            {
                var user = await httpClient.GetFromJsonAsync<UserDto>(url + "Users/GetById/"+selectedId);
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;
                txtAddress.Text = user.Address;
                txtEmail.Text = user.Email;
                txtPassword.Text = user.Password;
                comboBox1.SelectedValue = user.Gender==true? 1:2;
                txtUserName.Text = user.UserName;
                dateTimePicker1.Value = user.DateOfBirth;
                
            }
            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                UserUpdateDto userUpdateDto = new UserUpdateDto()
                {
                    Id=selectedId,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Address = txtAddress.Text,
                    DateOfBirth = Convert.ToDateTime(dateTimePicker1.Text),
                    Email = txtEmail.Text,
                    Gender = comboBox1.Text == "Erkek" ? true : false,
                    Password = txtPassword.Text,
                    UserName = txtUserName.Text,
                };
                HttpResponseMessage responseMessage = await httpClient.PutAsJsonAsync(url + "Users/Update", userUpdateDto);
                if (responseMessage.IsSuccessStatusCode)
                {
                    await DataGridViewFill();
                    MessageBox.Show("Düzenleme İşlemi Başarılı");
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Başarısız Düzenleme");
                }
            }
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage responseMessage = await httpClient.DeleteAsync(url + "Users/Delete/" + selectedId);
                if (responseMessage.IsSuccessStatusCode)
                {
                    await DataGridViewFill();
                    MessageBox.Show("Silme İşlemi Başarılı");
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Başarısız Silme");
                }
            }
        }
    }
        #endregion


    
}
