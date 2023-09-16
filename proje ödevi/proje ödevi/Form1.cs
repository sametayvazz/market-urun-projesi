using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje_ödevi
{
    public partial class Form1 : Form
    {
        public class dugum
        {
            public string ad;
            public int kod;
            public int fiyat;           //Samet Ayvaz 190508064
            
            public dugum sonraki;
            public dugum onceki;                       
        }
        dugum ilk = null;
        dugum son = null;

        public Form1()
        {
            InitializeComponent();
        }
        
        int indisec;
       
        private void button1_Click(object sender, EventArgs e) //ekleme
        {
            int no = Convert.ToInt32(textBox1.Text);
            dugum yeni = new dugum();                              //yeni adında değişken ürettik.          
            yeni.kod = Convert.ToInt32(textBox1.Text);        //yenin kod adındaki parametresi,int e çevirdik
            yeni.ad = textBox2.Text;                         //yenin ad adındaki parametresi
            yeni.fiyat = Convert.ToInt32(textBox3.Text);     //yenin fiyat adındaki parametresi, int e çevirdik
            
            if (ilk == null)     //ilk null ise daha önce veri tanımlanmamış demektir.
            {
                ilk = yeni;                   // ilk yeni veri
                son = ilk;                 // son aynı zamanda ilk oluyor
                ilk.onceki = null;          //ilkin öncekisi null
                son.sonraki = null;         //sondan sonraki null
                

            }
             else 
            {
                                              
                son.sonraki = yeni;         //son eklenen yenidir
                yeni.onceki = son;         //yeni eklenenden önceki sondur
                son = yeni;                 //yeni düğüm oluştu sonuncu yeni olmuş oldu
                son.sonraki = null;        //sondaki null
                
            }
            for (int satir = 0; satir < dataGridView1.Rows.Count; satir++)    
            {
                for (int  sutun = 0; sutun < dataGridView1.Columns.Count; sutun++)  //Satırlar ve sütunlar kontrol ediliyor.
                {
                    if (dataGridView1.Rows[satir].Cells[sutun].Value != null &&
                      dataGridView1.Rows[satir].Cells[sutun].Value.Equals(textBox1.Text.Trim()))
                    {                                                           //Aynı kod varsa,
                        MessageBox.Show("Lütfen Başka Bir Kod Giriniz..!!");  //Mesajı geliyor.
                        textBox1.Clear();                                     //Kod textbox'ını temizleme.
                        return;
                    }
                }
            }
        
        }
           
    private void button2_Click(object sender, EventArgs e) //silme            
        {
            int kod = Convert.ToInt32(textBox4.Text);         //stringi int'e çevirdik    
            dugum silinecek = new dugum();           //silinecek adında değişken ürettik
            dugum gecici = new dugum();               //gecici adında değişken ürettik
            silinecek = ilk;        //silineceği ilk'e atadık.

            if (ilk.kod == kod)     //ilk no , no'ya eşitse  ( ilk düğümü silme)
            {
                   ilk = ilk.sonraki;              //ilk veri ilkin sonrakisindaki olur
                   silinecek.onceki = null;           //listenin başındaki null
               }
               else if (son.kod == kod)         //son no , sileceğimiz no'ya eşitse  (son düğümü silme)
               {
                   son = son.onceki;    //sondan önceki son olur
                   son.sonraki = null;    //sondan sonraki listenin sonundaki null
               }
               else
               {
                   while (silinecek.kod != kod)     //silinecek no, girdiğimiz no'ya eşit değilse içeriye gir. (ortadan düğüm silme)
                   {
                       gecici = silinecek;         //geçiciyi silinecek olarak atıyor
                       silinecek = silinecek.sonraki;   //bir sonraki veriyi geçiçi olarak atıyor.
                   }
                   silinecek.onceki.sonraki = silinecek.sonraki;   //silinecek veriden sonraki, geçiciden sonraki veri olur
                   silinecek.sonraki.onceki = silinecek.onceki;     //sileceğimiz veriden önceki veriyi, sileceğimiz veriden sonraki verinin öncekisi olması kodu
               }
            indisec = dataGridView1.CurrentCell.RowIndex;  //listeden verinin üstüne tıklıyoruz
            dataGridView1.Rows.RemoveAt(indisec);   //sil diyip siliyoruz .

        }

        private void button3_Click(object sender, EventArgs e) //güncellemeyi verinin üstüne tıklıyoruz
            {                                                  //sonra verileri girip güncelle diyoruz.
                                                            

            DataGridViewRow guncelle = dataGridView1.Rows[indisec]; //verinin indisine göre tıklayıp güncelliyoruz
            guncelle.Cells[0].Value = textBox7.Text;   //0. indisi textbox7 deki veriyle değiştir
            guncelle.Cells[1].Value = textBox8.Text;   //1. indisi textbox8 deki veriyle değiştir
            guncelle.Cells[2].Value = textBox9.Text;   //2. indisi textbox9 deki veriyle değiştir


        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indisec = e.RowIndex;
            DataGridViewRow indis = dataGridView1.Rows[indisec];
            textBox4.Text = indis.Cells[0].Value.ToString(); //0. indisi textbox 4'e yazdı
            textBox5.Text = indis.Cells[1].Value.ToString(); //1. indisi textbox 5'e yazdı
            textBox6.Text = indis.Cells[2].Value.ToString(); //2. indisi textbox 6'e yazdı

            textBox7.Text = indis.Cells[0].Value.ToString(); //0. indisi textbox 7'e yazdı
            textBox8.Text = indis.Cells[1].Value.ToString(); //1. indisi textbox 8'e yazdı
            textBox9.Text = indis.Cells[2].Value.ToString(); //2. indisi textbox 9'e yazdı

        }
        private void listeyiYazdir(dugum ilk)   //listeye veriyi yazdırma
        {
            
            while (ilk != null)  // ilk sayı null a denk gelmiyorsa
            {
            
             dataGridView1.Rows.Add(textBox1.Text,textBox2.Text,textBox3.Text);
                ilk = ilk.sonraki;       // ilk veri ilkin sonrakisi oluyor.
                textBox1.Clear();            //textboxları temizleme
                textBox2.Clear();           //textboxları temizleme
                textBox3.Clear();           //textboxları temizleme            
            }
            
                       
        }        
        private void button6_Click(object sender, EventArgs e)
         {
                listeyiYazdir(son);          //listeye yazdir diye bir fonksiyon yazıp,listenin son
                                             //düğümünü gönderdi.
         }

        private void button4_Click(object sender, EventArgs e) //silin bul
        {

            //Bonus şeklinde yaptım hocam. Datagridview üzerinde tıklandığında textboxlara yazıyor.

        }
        private void button5_Click(object sender, EventArgs e) //güncelle bul
        {
            //Bonus şeklinde yaptım hocam. Datagridview üzerinde tıklandığında textboxlara yazıyor.
        }  
               
    }
}

    
 
    



