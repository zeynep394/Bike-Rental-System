using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje3
{
    class müşteri
    {
        public int id;
        public string saat;

        public müşteri(int id, string saat)
        {
            this.id = id;
            this.saat = saat;
        }
    }
    class Durak
    {
        public string durakAdı;
        public int boşPark;
        public int tandemBisiklet;
        public int normalBisiklet;
        public List<müşteri> müşteriListesi;

        public Durak(string durakAdı, int boşPark, int tandemBisiklet, int normalBisiklet, List<müşteri> müşteriListesi)
        {
            this.durakAdı = durakAdı;
            this.boşPark = boşPark;
            this.tandemBisiklet = tandemBisiklet;
            this.normalBisiklet = normalBisiklet;
            this.müşteriListesi = müşteriListesi;
        }
    }

    class TreeNode
    {
        public Durak data;
        public TreeNode leftChild;
        public TreeNode rightChild;
        public void displayNode()
        {
            Console.Write("Durak Adı: ");
            Console.WriteLine(" " + data.durakAdı + " ");
            Console.Write("Boş Park Sayısı: ");
            Console.WriteLine(" " + data.boşPark + " ");
            Console.Write("Tandem Bisiklet Sayısı: ");
            Console.WriteLine(" " + data.tandemBisiklet + " ");
            Console.Write("Normal Bisiklet Sayısı: ");
            Console.WriteLine(" " + data.normalBisiklet + " ");
            Console.Write("Kiralama Yapan Müşteri Sayısı: ");
            Console.WriteLine(" " + data.müşteriListesi.Count + " ");
            foreach (müşteri item in data.müşteriListesi)
            {
                Console.WriteLine("{0},{1} ", item.id, item.saat);

            }

            Console.WriteLine(" ");
        }
    }

    class Tree
    {
        public TreeNode root;
        public int sayi;

        public Tree()
        {

            root = null;
        }

        public TreeNode getRoot()
        { return root; }



        public void inOrder(TreeNode localRoot) //recursive metodunu kullanarak ağacı dolaşan ve küçükten büyüğe yazdıran inOrder methodu
        {
            if (localRoot != null)
            {
                inOrder(localRoot.leftChild);
                localRoot.displayNode();
                inOrder(localRoot.rightChild);
            }
        }

        public void insert(Durak newdata) //verilen nesneyi ağaca ekleyen method
        {
            TreeNode newNode = new TreeNode();
            newNode.data = newdata; 
            if (root == null) //eğer root null ise ağaç boştur ve gelen eleman roota atanır.
                root = newNode;
            else
            {
                TreeNode current = root; //değilse rootu dolaşmak için bir current node'u tanımlanır.
                TreeNode parent;

                while (true)
                {
                    /*s1==s2 returns 0  
                    s1>s2 returns 1  
                    s1<s2 returns -1*/

                    int compareString = String.Compare(newdata.durakAdı, current.data.durakAdı); //string değerleri karşılaştıran ve s1<s2 ise -1 döndüren kod parçası

                    parent = current;
                    if (compareString == -1)//eğer s1<s2 ise
                    {
                        current = current.leftChild; //ağaç yapısına göre küçük eleman sol çocukta bulunur bu yüzden current'i solÇocuk olarak atıyoruz
                        if (current == null)//current null ise onun yerine gelen elemanı yeni bir çocuk olarak tanımlıyoruz.
                        {
                            parent.leftChild = newNode;
                            return;
                        }
                    }
                    else
                    {
                        current = current.rightChild;
                        if (current == null)
                        {
                            parent.rightChild = newNode;
                            return;
                        }
                    }
                }
                
            } 
        } // end insert()

        public int maxDepth(TreeNode node)
        {
            if (node == null)
                return 0;
            else
            {
                /* compute the depth of each subtree */
                int lDepth = maxDepth(node.leftChild);
                int rDepth = maxDepth(node.rightChild);

                /* use the larger one */
                if (lDepth > rDepth)
                    return (lDepth + 1);
                else
                    return (rDepth + 1);
            }
        }
    }

    class Public
    {
        static String[] duraklar = { "İnciraltı, 28, 2, 10", "Sahilevleri, 8, 1, 11", "Doğal Yaşam Parkı, 17, 1, 6", "Bostanlı İskele, 0, 0, 5", "Bayraklı İskele, 2, 2, 8", "Fuar Montrö, 10, 1, 11", "Saygun, 9, 1, 10", "Bornova Metro, 6, 2, 7", "Konak Metro, 4, 3, 8" };
        static Random random = new Random();

        public static Durak[] DurakNesnesiOluştur(String[] duraklar)  // dosyadan banknot bilgilerini okuyan ve döndüren metot
        {
            Durak[] Duraklar = new Durak[duraklar.Length]; //durak nesneleri tutmak için Duraklar listesi tanımlanıyor


            //verilen dizinin tüm elemanları duraklar dizisine atılıyor.
            for (int i = 0; i < duraklar.Length; i++)
            {
                String stringdeger = duraklar[i];//Her bir satır stringdeger değişkenine atanıyor.
                string[] DurakNesneBilgileri = stringdeger.Split(','); // her bir satırdan okunan veriler, virgülden parçalanıp diziye atılıyor
                string durakAdı = DurakNesneBilgileri[0];
                int boşPark = Convert.ToInt32(DurakNesneBilgileri[1]);
                int tandemBisiklet = Convert.ToInt32(DurakNesneBilgileri[2]);
                int normalBisiklet = Convert.ToInt32(DurakNesneBilgileri[3]);

                //her nesne için bir tane random sayıda eleman içeren müşteri listesi oluşturuluyor
                int müşteriSayısı = random.Next(1, 11); //1 ile 10 arasında müşteri sayısı belirliyor
                List<müşteri> müşteriListesi = new List<müşteri>(müşteriSayısı); //her döngüde yeni müşteriListesi oluşacak her durak nesnesi için yeni bir müşteri listesi olacak

                for (int j = 0; j < müşteriSayısı; j++)
                {
                    int id = random.Next(1, 20);

                    var minutes = random.Next(0, 18 * 60);
                    var timeOfDay = TimeSpan.FromMinutes(minutes);
                    var dt = new DateTime(2021, 01, 25) + timeOfDay;
                    string saat = dt.ToString("hh:mm tt");

                    müşteri müşteriNesnesi = new müşteri(id, saat);
                    müşteriListesi.Add(müşteriNesnesi);


                }
                int topbs = normalBisiklet + tandemBisiklet;
                if (müşteriSayısı < topbs)
                {
                    topbs = topbs - müşteriSayısı;
                    boşPark += müşteriSayısı;
                    if (normalBisiklet - müşteriSayısı < 0)
                    {
                        normalBisiklet = 0;
                        tandemBisiklet = tandemBisiklet - (normalBisiklet - müşteriSayısı);
                    }
                    else
                    {
                        normalBisiklet -= müşteriSayısı;
                    }
                }
                else
                {
                    int artanMüşteri = müşteriSayısı - topbs;
                    int rangeremove = müşteriListesi.Count - artanMüşteri;
                    müşteriListesi.RemoveRange(rangeremove, artanMüşteri);

                    /*for (int k = 0; k < artanMüşteri; k++)
                    {
 //son artanmüşterininci elemandan artan müşteri sayısı kadar sil

                    }*/
                    normalBisiklet = 0;
                    tandemBisiklet = 0;
                }
                //müşteri sayısı kadar 1 ile 20 arasında random id belirleyecek

                Durak durakNesnesi = new Durak(durakAdı, boşPark, tandemBisiklet, normalBisiklet, müşteriListesi); // yukarıda atanan değerler ile her dönüşte bir durak nesnesi oluşturuluyor
                Duraklar[i] = durakNesnesi;

                DurakNesneBilgileri = new string[DurakNesneBilgileri.Length]; // her satırı tutmak için kullanılan diziyi for döngüsününe gitmeden önce boşaltıyoruz
            }

            return Duraklar; //durak nesnelerini içeren dizi döndürülüyor (bu örnek için 9 tane durak nenesi oluşturuldu)
        }

        public static void AğacıDolaşma(TreeNode node, int arananID)
        {//Aranan ID’yi ve node’U parametre olarak alan AğacıDolaşma methodu 


            if (node != null)
            {
                AğacıDolaşma(node.leftChild, arananID);// Ağacı dolaşmak için bu metodu sol çocuk ve sağ çocuk olmak üzere recursive olarak çağırıyoruz
                List<müşteri> müşteriListesi = node.data.müşteriListesi;//elemanları yazdırmak için tanımlanan müşteriListesi listesi 
                foreach (müşteri eleman in müşteriListesi)
                {
                    if (eleman.id == arananID)
                    {
                        Console.WriteLine($"{eleman.id} ID'li müşteri : ");
                        Console.Write("Durak Adı: ");
                        Console.WriteLine(node.data.durakAdı);
                        Console.Write("saat: ");
                        Console.WriteLine(eleman.saat);

                    }
                }
                AğacıDolaşma(node.rightChild, arananID);// Ağacı dolaşmak için bu metodu sol çocuk ve sağ çocuk olmak üzere recursive olarak çağırıyoruz
            }

        }

        public static Tree AğacıListele()
        {
            Durak[] Duraklar = new Durak[duraklar.Length];
            Duraklar = DurakNesnesiOluştur(duraklar);

            Tree ağaç = new Tree();

            foreach (Durak item1 in Duraklar)
            {
                ağaç.insert(item1);
            }
            Console.Write("\nAgacın InOrder Dolasılması : ");

            ağaç.inOrder(ağaç.getRoot());
            Console.Write("\nAgacın Derinliği : ");
            Console.WriteLine(ağaç.maxDepth(ağaç.getRoot()));
            return ağaç;

        }

        public static void MüşteriEkle(TreeNode node, int arananID, string arananİstasyon)
        {
            if (node != null)
            {

                String istasyonAdı = node.data.durakAdı; //Ağaçtaki istasyon adlarını tutmak için tanımlanan istasyonAdı değişkeni
                List<müşteri> müşteriListesi = node.data.müşteriListesi;
                if (istasyonAdı == arananİstasyon) //arananİstasyon ile ağaçtaki istasyon isimleri karşılaştırılıyor.
                {
                    if (node.data.normalBisiklet > 0) //Müşterinin kiralayabileceği bisiklet varsa müşteri bisikleti kiralayınca bisiklet sayısı 1 azalıyor ve boş park sayısı 1 artıyor.
                    {
                        node.data.normalBisiklet -= 1;
                        node.data.boşPark += 1;

                        //Random saat ataması yapılıyor
                        var minutes = random.Next(0, 18 * 60);
                        var timeOfDay = TimeSpan.FromMinutes(minutes);
                        var dt = new DateTime(2021, 01, 25) + timeOfDay;
                        string saat = dt.ToString("hh:mm tt");

                        müşteri item = new müşteri(arananID, saat); //yeni gelen müşteri nesnesi müştei Listesine ekleniyor.
                        müşteriListesi.Add(item);
                    }
                    else
                    {
                        Console.WriteLine("Bu durakta boş bisiklet kalmamıştır! ");
                    }
                    Console.WriteLine("Yeni Müşteri Eklendikten Sonra Müşteri Listesi:  ");
                    foreach (müşteri eleman in müşteriListesi)
                    {

                        Console.Write("id: ");
                        Console.WriteLine(eleman.id);

                        Console.Write("saat: ");
                        Console.WriteLine(eleman.saat);

                    }
                    Console.WriteLine("");
                }
                // Ağacı dolaşmak için bu metodu sol çocuk ve sağ çocuk olmak üzere recursive olarak çağırıyoruz
                MüşteriEkle(node.leftChild, arananID, arananİstasyon);



                MüşteriEkle(node.rightChild, arananID, arananİstasyon);
            }
        }

        public static void HashTableOluştur(Durak[] Duraklar)
        {

             Hashtable durakHashTable = new Hashtable();
            //durak nesneleri tutmak için Duraklar listesi tanımlanıyor


            //verilen dizinin tüm elemanları duraklar dizisine atılıyor.
            for (int i = 0; i < Duraklar.Length; i++)
            {
                durakHashTable.Add(Duraklar[i].durakAdı, Duraklar[i]);

                if (Duraklar[i].boşPark > 5)
                {
                    int normalbisiklet = Duraklar[i].normalBisiklet;
                    normalbisiklet += 5;
                    Duraklar[i].normalBisiklet = normalbisiklet;
                }
            }
            ICollection values = durakHashTable.Values;

            ICollection keys = durakHashTable.Keys;
            String[] keydurakadı=new String[durakHashTable.Count];
            int sayac = 0;
            foreach(string k in keys)
            {
                keydurakadı[sayac]=k;
                sayac += 1;
            }
            int indexkey = 0;
            foreach (Durak item in values) {
                Console.Write("Key:");
                Console.WriteLine(keydurakadı[indexkey]);
                Console.WriteLine($"Boş Park Yeri  Sayısı: {item.boşPark}");
                Console.WriteLine($"Tandem Bisiklet Sayısı: {item.tandemBisiklet}");
                Console.WriteLine($"Normal Bisiklet Sayısı: {item.normalBisiklet}");
                indexkey += 1;
                Console.WriteLine(" ");
            }

        }
        static void Main(string[] args)
        {

            Tree ağaç = AğacıListele();

            Console.WriteLine("Bulmak istedğiniz ID'yi giriniz");
            int arananID = Convert.ToInt32(Console.ReadLine());

            AğacıDolaşma(ağaç.getRoot(), arananID);

            Console.WriteLine("Bisiklet kiralamk istedğiniz istasyon adını giriniz");
            String arananİstasyon = Console.ReadLine();

            Console.WriteLine("Eklemek istedğiniz ID'yi giriniz");
            int eklenecekID = Convert.ToInt32(Console.ReadLine());

            MüşteriEkle(ağaç.getRoot(), eklenecekID, arananİstasyon);

            Durak[] Duraklar = DurakNesnesiOluştur(duraklar);
            HashTableOluştur(Duraklar);

            Console.ReadKey();
        }
    }
}
