using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje3_HeapEkleme
{
    class Durak
    {
        public string durakAdı;
        public int boşPark;
        public int tandemBisiklet;
        public int normalBisiklet;


        public Durak(string durakAdı, int boşPark, int tandemBisiklet, int normalBisiklet)
        {
            this.durakAdı = durakAdı;
            this.boşPark = boşPark;
            this.tandemBisiklet = tandemBisiklet;
            this.normalBisiklet = normalBisiklet;

        }
    }
    class Node
    {
        public Durak data;

        public Node(Durak data)
        {
            this.data = data;
        }

        public Durak getdata()
        {
            return data;
        }
    }
    class Heap
    {
        //methodalrın yazılacağı ve heapin oluşturulacağı program
        //insert trickleup remove trickledown methodları ve heap için gerekli değişkenleri içeriyor.

        Node[] heapArray;
        int maxSize;
        int currentSize;

        public Heap(int maxSize)
        {
            this.maxSize = maxSize;
            currentSize = 0;
            heapArray = new Node[maxSize];

        }

        public bool insert(Durak eleman)
        {//bu elemanı heap'e ekle

            if (currentSize == maxSize)
            { //currentSize maxSize a eşitse array full dolu demektir.
                return false;
            } //

            Node yeniEleman = new Node(eleman);
            heapArray[currentSize] = yeniEleman;
            trickleUp(currentSize++);
            return true;
        }

        public void trickleUp(int index)
        {

            int parent = (index - 1) / 2;
            Node bottom = heapArray[index];

            while (index > 0 & heapArray[parent].getdata().normalBisiklet < bottom.data.normalBisiklet)
            {
                heapArray[index] = heapArray[parent];
                index = parent;
                parent = (parent - 1) / 2;
            }
            heapArray[index] = bottom;
        }

        public Node remove() // Öncelik en büyük değerli elemanda olduğu için ilk silinecek eleman köktür daha sonra boşluğa eklenecek eleman trickledown methoduyla belirleniyor
        { // 
            Node root = heapArray[0];
            heapArray[0] = heapArray[--currentSize];
            trickleDown(0);

            Console.Write("Boş Park Sayısı: ");
            Console.WriteLine(root.data.boşPark);
            Console.Write("Durak Adı: ");
            Console.WriteLine(root.data.durakAdı);
            Console.Write("Normal Bisiklet Sayısı: ");
            Console.WriteLine(root.data.normalBisiklet);
            Console.Write("Tandem Bisiklet Sayısı: ");
            Console.WriteLine(root.data.tandemBisiklet);

            return root;
        } // end remove()

        public void trickleDown(int index)
        {
            int largerChild;
            Node top = heapArray[index]; // save root
            while (index < currentSize / 2) // while node has at
            { // least one child,
                int leftChild = 2 * index + 1;
                int rightChild = leftChild + 1; //leftChild'ın sağındaki node


                if (rightChild < currentSize && heapArray[leftChild].getdata().normalBisiklet < heapArray[rightChild].getdata().normalBisiklet)

                    largerChild = rightChild;
                else
                    largerChild = leftChild;


                if (top.getdata().normalBisiklet >= heapArray[largerChild].getdata().normalBisiklet)
                    break;

                heapArray[index] = heapArray[largerChild];
                index = largerChild; // go down
            } // end while
            heapArray[index] = top; // root to index


        }

    }
    class Program
    {
        static String[] duraklar = { "İnciraltı, 28, 2, 10", "Sahilevleri, 8, 1, 11", "Doğal Yaşam Parkı, 17, 1, 6", "Bostanlı İskele, 7, 0, 5", "Bayraklı İskele, 7, 2, 15", "Fuar Montrö, 10, 1, 12", "Saygun, 9, 1, 13", "Bornova Metro, 6, 2, 7", "Konak Metro, 4, 3, 8" };
        static Random random = new Random();
        static int[] theArray = new int[duraklar.Length];
        public static Durak[] DurakNesnesiOluştur(String[] duraklar)  // duraklar dizisinden okuduğu bilgileri nesne olarak Duraklar dizisine ekleyen metod
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

                theArray[i]=normalBisiklet;
                //her nesne için bir tane random sayıda eleman içeren müşteri listesi oluşturuluyor

                //müşteri sayısı kadar 1 ile 20 arasında random id belirleyecek
                
                Durak durakNesnesi = new Durak(durakAdı, boşPark, tandemBisiklet, normalBisiklet); // yukarıda atanan değerler ile her dönüşte bir durak nesnesi oluşturuluyor
                Duraklar[i] = durakNesnesi;

                DurakNesneBilgileri = new string[DurakNesneBilgileri.Length]; // her satırı tutmak için kullanılan diziyi for döngüsününe gitmeden önce boşaltıyoruz
            }

            return Duraklar; //durak nesnelerini içeren dizi döndürülüyor (bu örnek için 9 tane durak nenesi oluşturuldu)
        }



        public static Durak[] selectionSort(Durak[] Duraklar)
        {
            
            
            Durak[] copyArray = new Durak[Duraklar.Length]; //Sırlanacak dizi ve eleman çekilecek dizi birbirinden farklı olacak
            Duraklar.CopyTo(copyArray,0);//Duraklar dizisinin kopyasını alarak sıralanacak copyArray dizisi oluşturuluyor.
            
            for (int i = 0; i < copyArray.Length; i++)//copyArray'in tüm elemanlarını dolaşarak sıralama yapacak döngü
            {
                string min = "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ"; //Dizideki tüm string değerlerinden büyük bir değeri başlangıç min değeri olarak atıyor
                int sayac = 0;
                Durak copyEleman = copyArray[i]; //
                foreach (Durak item in Duraklar) {//Her seferinde minimum elemanı bulup listenin sol sonundan başlayarak yerleştiren metod
                    /*s1==s2 returns 0  
s1>s2 returns 1  
s1<s2 returns -1*/

                    int compareString = String.Compare(item.durakAdı, min);// string değerlerinin karşılaştırılmasını sağlayan kod. s1<s2 ise -1 döndürüyor.
                    if (compareString == -1)
                    {
                        min = item.durakAdı; //min eleman bulunuyor
                        
                        
                        sayac = Array.IndexOf(copyArray, item);
                        copyArray[i] = item; //oluşturulan copyArray dizisi üzerinde min eleman ile min elemanın yerindeki eleman yer değiştiriyor
                        Duraklar[0] = item;
                    }

                }

                
                copyArray[sayac] = copyEleman;
                Duraklar[sayac - i] = copyEleman; //bulunan min eleman yeri belirlendikten sonra Duraklar dizisinden siliniyor
                List<Durak> duraklar = Duraklar.ToList();
                duraklar.RemoveAt(0);
                Duraklar = duraklar.ToArray();
                

            }
            
            return copyArray;
        }
        public static void display(Durak[] Duraklar) // displays array contents
        {
            Console.WriteLine("A =");
            foreach (Durak item in Duraklar)
            {
                Console.Write("Key:");
                Console.WriteLine(item.durakAdı);
                Console.WriteLine($"Boş Park Yeri  Sayısı: {item.boşPark}");
                Console.WriteLine($"Tandem Bisiklet Sayısı: {item.tandemBisiklet}");
                Console.WriteLine($"Normal Bisiklet Sayısı: {item.normalBisiklet}");

                Console.WriteLine(" ");
            }
            /*for (int j = 0; j < Duraklar.Length; j++) // for each element,
                Console.WriteLine(Duraklar[j]); // display it*/
            Console.WriteLine("");
        }
        public static void displayquicksort(int[] theArray) // displays array contents
        {
            
            for (int j = 0; j < theArray.Length; j++) // for each element,
                Console.Write(theArray[j] + " "); // display it
            Console.WriteLine("");
            
        }

        public static void quickSort()
        {
            recQuickSort(0, theArray.Length - 1,theArray);
        }
        //--------------------------------------------------------------
        public static void recQuickSort(int left, int right, int[] theArray)
        {
            if (right - left <= 0) // if size <= 1,
                return; // already sorted
            else // size is 2 or larger
            {
                long pivot = theArray[right]; // rightmost item
                                              // partition range
                int partition = partitionIt(left, right, pivot,theArray);
                recQuickSort(left, partition - 1,theArray); // sort left side
                recQuickSort(partition + 1, right,theArray); // sort right side
            }
        } // end recQuickSort()
          //--------------------------------------------------------------
        public static int partitionIt(int left, int right, long pivot, int[] theArray)
        {
            int leftPtr = left - 1; // left (after ++)
            int rightPtr = right; // right-1 (after --)
            while (true)
            { // find bigger item
                while (theArray[++leftPtr] < pivot)
                    ; // (nop)

                // find smaller item
                while (rightPtr > 0 && theArray[--rightPtr] > pivot)
                    ; // (nop)
                if (leftPtr >= rightPtr) // if pointers cross,
                    break; // partition done
                else // not crossed, so
                    swap(leftPtr, rightPtr,theArray); // swap elements
            } // end while(true)
            swap(leftPtr, right,theArray); // restore pivot
            return leftPtr; // return pivot location
        }

        public static void swap(int dex1, int dex2,int[] theArray) // swap two elements
        {
            int temp = theArray[dex1]; // A into temp
            theArray[dex1] = theArray[dex2]; // B into A
            theArray[dex2] = temp; // temp into B
        }



        static void Main(string[] args)
        {
            Durak[] Duraklar = DurakNesnesiOluştur(duraklar);

            int maxSize = 9;
            Heap heapArray = new Heap(maxSize);

            foreach(Durak nesne in Duraklar)
            {
                heapArray.insert(nesne);
            }
            Console.WriteLine("Silinen 3 eleman:");
            for(int i = 0; i < 3; i++)
            {
                heapArray.remove();
                Console.WriteLine(" ");
            }

            Console.WriteLine("********************** AFTER SELECTION SORT *************************");
            Durak[] values = selectionSort(Duraklar);
            foreach (Durak item in values)
            {
                Console.Write("Key:");
                Console.WriteLine(item.durakAdı);
                Console.WriteLine($"Boş Park Yeri  Sayısı: {item.boşPark}");
                Console.WriteLine($"Tandem Bisiklet Sayısı: {item.tandemBisiklet}");
                Console.WriteLine($"Normal Bisiklet Sayısı: {item.normalBisiklet}");
                
                Console.WriteLine(" ");
            }
            /*Durak[] Duraklar2 = DurakNesnesiOluştur(duraklar);
            display(Duraklar2); // display items
            quickSort(Duraklar2); // quicksort them
            display(Duraklar2);
            Console.ReadLine();*/
            Console.WriteLine("********************** QUICK SORT *************************");
            Console.Write("Unsorted Array = ");
            displayquicksort(theArray);
            quickSort();
            Console.Write("Array After QuickSort = ");
            displayquicksort(theArray);

            Console.ReadLine();
        }
    }
}
