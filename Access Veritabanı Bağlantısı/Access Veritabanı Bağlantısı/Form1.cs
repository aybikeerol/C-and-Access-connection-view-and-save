using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Access_Veritabanı_Bağlantısı
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/ADMIN/Desktop/Kişiler.mdb");
        private void verileri_görüntüle()
        {
            listView1.Items.Clear();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();//komut vermek için sql le cok benziyor orada new sqlcommand komut 
            komut.Connection = baglanti;//komut ile bağlantımı ilişkilendireceğim.
            komut.CommandText=("Select * From Bilgiler");//hangi tablodan veri çekeceğim
            OleDbDataReader oku = komut.ExecuteReader();//verileri okur
            while (oku.Read())//oku komutu çalıştığı sürece ne olması gerekse yazılır.
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["Ad"].ToString();
                ekle.SubItems.Add(oku["Soyad"].ToString());
                ekle.SubItems.Add(oku["Yaş"].ToString());
                ekle.SubItems.Add(oku["İlçe"].ToString());
                //tablodaki bilgileri ekledik fakat ilk eleman eklenirken Text kullandık
                //diğerleri için SubItems kullandık.

                listView1.Items.Add(ekle);
                //değerleri listview e ekler.

            }
            baglanti.Close();


        }


        private void button1_Click(object sender, EventArgs e)
        {
            verileri_görüntüle();//burada yazdıklarımızı çağırdık.
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //Kaydet bölümü
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("INSERT INTO Bilgiler(Ad,Soyad,Yaş,İlçe) values('"+textBox1.Text.ToString()+"','"+textBox2.Text.ToString()+"','"+textBox3.Text.ToString()+"','"+textBox4.Text.ToString()+"')",baglanti);
            //her textbox ı ilgili degerle ilişkilendirdik.textbox1=Ad mesela.
            komut.ExecuteNonQuery();
            baglanti.Close();
            verileri_görüntüle();//yenileme için
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();




        }
    }
}
