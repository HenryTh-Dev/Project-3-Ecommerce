using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace dotframe
{
    public class Program
    {
        struct PurchasedProductInfo
        {

            public string prodName;
            public float prodPrice;
            public int qntt;

        }
        [System.Serializable]
        struct ProductInfo
        {
            
            public string prodName;
            public float prodPrice;
            
            
        }
        [System.Serializable]
        struct ClientData
        {
            public string email;
            public string password;
            public string firstName;
            public string lastName;
            public string ssn;
            public string phone;
            public string birthYear;
            
        }
        static string loggedUserEmail = "";
        
        
        



        static List<ClientData> clients = new List<ClientData>();
        static List<ProductInfo> ProdInfo = new List<ProductInfo>();
        static List<PurchasedProductInfo> CartL = new List<PurchasedProductInfo>();
        enum Admin { addProd = 1, removeProd, ListProd, logoff }
        enum Confirm { TryAgain = 1, Cancel }
        enum MenuF { Login = 1, SignIn = 2, Exit }

        static void Main(string[] args)
        {
            
            MainMenu();
        }
        static void MainMenu()
        {
            LoadCLi();
            LoadProd();
            bool ChExit = false;
            while (ChExit == false)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Welcome to Hebra E-COMMERCE");
                    Console.WriteLine("Please log in.");
                    Console.WriteLine("\n1-Login\n2-Don't have a account? Sign in!\n3-Exit");
                    int ChMenu = int.Parse(Console.ReadLine());

                    MenuF option = (MenuF)ChMenu;
                    switch (option)
                    {
                        case MenuF.Login:
                            Login();
                            break;
                        case MenuF.SignIn:
                            SignIn();
                            break;
                        case MenuF.Exit:
                            ChExit = true;
                            break;


                    }
                }catch
                {
                    MainMenu();
                }


                
            }
        }
        static void SignIn()
        {
            Console.Clear();
            ClientData client = new ClientData();
            Console.WriteLine("Welcome to Hebra E-COMMERCE");
            Console.WriteLine("Enter your email:");
            client.email = Console.ReadLine();
            var emailTest = client.email;
            var alreadyCreatedEmail = clients.Find(c => c.email == emailTest);

            if (emailTest == alreadyCreatedEmail.email)
            {
                Console.WriteLine("Email already registred\nPress ENTER to try again");
                Console.ReadLine();
                MainMenu();
            }
            Console.WriteLine("Create a password:");
            var firstPass = Console.ReadLine();
            Console.WriteLine("Confirm your password");
            var confirm = Console.ReadLine();
            if (firstPass != confirm)
            {
                Console.WriteLine("Incorrect password\nPress ENTER to try again");
                Console.ReadLine();
                SignIn();
            }
            client.password = firstPass;
            Console.WriteLine("Please enter your first name:");
            client.firstName = Console.ReadLine();
            Console.WriteLine("Enter your last name:");
            client.lastName = Console.ReadLine();
            Console.WriteLine("Enter your SNN:");
            client.ssn = Console.ReadLine();
            Console.WriteLine("Enter your phone:");
            client.phone = Console.ReadLine();
            Console.WriteLine("Enter your birth year:");
            client.birthYear = Console.ReadLine();
            Console.WriteLine("Account created\nPress ENTER to redirect to main menu");
            loggedUserEmail = client.email;

            clients.Add(client);
            
            SaveCli();
            logon();
            
        }
        static void Login()
        {
            Console.Clear();
            var AuthEmail = false;
            Console.WriteLine("Enter your email:");
            var typedEmail = Console.ReadLine();
            var realEmail = clients.Find(e => e.email == typedEmail);
            if (realEmail.email == typedEmail || typedEmail == "admin")
            {
                AuthEmail = true;
            }
            loggedUserEmail = realEmail.email;
            var AuthPass = false;
            Console.WriteLine("Enter your password:");
            var typedPass = Console.ReadLine();
            var realPass = clients.Find(c => c.password == typedPass);
            if (realPass.password == typedPass || typedPass == "admin")
            {
                AuthPass = true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Wrong Password\n\n");
                Console.WriteLine("Press ENTER to try again");
                Console.ReadLine();
                Login();
            }

            if (AuthEmail == true && AuthPass == true)
            {
                if (typedEmail == "admin" && typedPass == "admin")
                {
                    Console.Clear();
                    Console.WriteLine("Admin Login successfully!\nPress ENTER to continue");
                    Console.ReadLine();
                    
                    admin();
                    
                }
                Console.Clear();
                Console.WriteLine("Login successfully!\nPress ENTER to continue");
                Console.ReadLine();
                
                logon();
            }

        }
        enum MenuU { Catalog = 1, ChangePassword, MyCart, MyPerInfo, Logout }
        static void logon()
        {

            bool ChExit = false;
            while (!ChExit)
            {
                Console.Clear();

                Console.WriteLine("Welcome to Hebra E-COMMERCE");
                Console.WriteLine("You are logged in.");
                try
                {
                    Console.WriteLine("1-Product catalog\n2-Change Password\n3-My Cart\n4-My Personal Info\n5-Logout");
                    int ChMenu = int.Parse(Console.ReadLine());

                    MenuU option = (MenuU)ChMenu;
                    switch (option)
                    {
                        case MenuU.Catalog:
                            Catalog();
                            break;
                        case MenuU.ChangePassword:
                            ChangePassword();
                            break;
                        case MenuU.MyCart:
                            MyCart();
                            break;
                        case MenuU.MyPerInfo:
                            MyData();
                            break;
                        case MenuU.Logout:
                            ChExit = true;
                            break;

                            
                    }
                }
                catch
                {
                    logon();
                }
                Console.WriteLine("Press ENTER");
                


            }


        }
        static void ChangePassword()
        {

            Console.WriteLine("Welcome\nIn order to change your password enter your old password:");

            var oldPasswordConfirmation = Console.ReadLine();
            var oldPassword = clients.Find(c => c.password == oldPasswordConfirmation);
            if (oldPasswordConfirmation != oldPassword.password)
            {
                Console.WriteLine("Invalid password,\n1-Try again\n2-Cancel");
                int conf1 = int.Parse(Console.ReadLine());

                Confirm option1 = (Confirm)conf1;
                switch (option1)
                {
                    case Confirm.TryAgain:
                        ChangePassword();
                        break;
                    case Confirm.Cancel:
                        logon();
                        break;

                }
            }
                Console.WriteLine("Type your new password");
                var newPass = Console.ReadLine();
                if (newPass == oldPassword.password)
                {
                    Console.WriteLine("Old password entered, please use a new password\n1-Try again\n2-Cancel");
                    int conf = int.Parse(Console.ReadLine());

                    Confirm option = (Confirm)conf;
                    switch (option)
                    {
                        case Confirm.TryAgain:
                            ChangePassword();
                            break;
                        case Confirm.Cancel:
                            logon();
                            break;
                    }


                }
                Console.WriteLine("Confirm your new password:");
                var confirmNewPass = Console.ReadLine();
            if (confirmNewPass != newPass)
            {
                Console.WriteLine("Invalid password,\n1-Try again\n2-Cancel");
                int conf2 = int.Parse(Console.ReadLine());

                Confirm option2 = (Confirm)conf2;
                switch (option2)
                {
                    case Confirm.TryAgain:
                        ChangePassword();
                        break;
                    case Confirm.Cancel:
                        logon();
                        break;

                }
            }
            Console.WriteLine("Password changed!");
            oldPassword.password = newPass;
            
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();
            
            logon();

        }
        static void admin()
        {
            Console.Clear();
            Console.WriteLine("Welcome to ADMIN Menu!\n\n1-Add product\n2-Remove product\n3-Products list\n4-Logoff");
            int conf1 = int.Parse(Console.ReadLine());

            Admin option1 = (Admin)conf1;
            switch (option1)
            {
                case Admin.addProd:
                    addItem();
                    break;
                case Admin.removeProd:
                    removeItem();
                    break;
                case Admin.ListProd:
                    ItemList();
                    break;
                case Admin.logoff:
                    MainMenu();
                    
                    break;

            }


        }
        static void addItem()
        { 
            {
                
                Console.Clear();
                ProductInfo prod = new ProductInfo();
                Console.WriteLine("Product register");
                Console.WriteLine("Product name:");
                prod.prodName = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Product price:");
                prod.prodPrice = float.Parse(Console.ReadLine());
                Console.Clear();


                ProdInfo.Add(prod);
                SaveProd();
                admin();
            }  
        }
        static void removeItem()
        {
            Console.Clear();
            int i = 0;
            Console.WriteLine($"-----------------------------------");
            foreach (ProductInfo prod in ProdInfo)
            {
                Console.WriteLine($"Prod Index ID:{i}");
                Console.WriteLine($"Prod Name: {prod.prodName}");
                Console.WriteLine($"Prod Price: {prod.prodPrice}");
                i++;
                Console.WriteLine($"-----------------------------------");
            }
            Console.WriteLine("Type the Product ID that you want to remove:");
            int id = int.Parse(Console.ReadLine());
            ProdInfo.RemoveAt(id);
            Console.WriteLine("Successfully removed");
            Console.WriteLine("Press ENTER to return");
            Console.ReadLine();
            SaveProd();
            admin();
        }
        static void ItemList()
        {
            Console.Clear();
            int i = 0;
            
            if (ProdInfo.Count < 1)
            {
                Console.WriteLine("No products registred.");
                Console.ReadLine();
            }
            Console.WriteLine($"-----------------------------------");
            foreach (ProductInfo prod in ProdInfo)
            {
                Console.WriteLine($"Prod Index ID:{i}");
                Console.WriteLine($"Prod Name: {prod.prodName}");
                Console.WriteLine($"Prod Price: {prod.prodPrice}");
                i++;
                Console.WriteLine($"-----------------------------------");

            }
            Console.WriteLine("Press ENTER to return");
            Console.ReadLine();

            admin();

        }
        /*static void UpdateCli()
        {
            FileStream stream = new FileStream("clients.data", FileMode.Append);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clients);
           

            stream.Close();


        }*/
        static void SaveCli()
        {
            FileStream stream = new FileStream("clients.data", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clients);

            stream.Close();


        }
        static void SaveProd()
        {
            FileStream stream = new FileStream("products.data", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, ProdInfo);

            stream.Close();


        }
        static void LoadCLi()
        {
            FileStream stream = new FileStream("clients.data", FileMode.OpenOrCreate);
            try
            {
                BinaryFormatter encoder = new BinaryFormatter();

                clients = (List<ClientData>)encoder.Deserialize(stream);

                if (clients == null)
                {
                    clients = new List<ClientData>();

                }


            }
            catch (Exception ex)
            {
                clients = new List<ClientData>();

            }

            stream.Close();
        }
        static void LoadProd()
        {
            FileStream stream = new FileStream("products.data", FileMode.OpenOrCreate);
            try
            {
                BinaryFormatter encoder = new BinaryFormatter();

                ProdInfo = (List<ProductInfo>)encoder.Deserialize(stream);

                if (ProdInfo == null)
                {
                    ProdInfo = new List<ProductInfo>();

                }


            }
            catch (Exception ex)
            {
                ProdInfo = new List<ProductInfo>();

            }

            stream.Close();
        }
        static void MyData()
        {
            Console.Clear();
            var userData = clients.Find(c => c.email == loggedUserEmail);
            Console.WriteLine($"Your Personal Information:\n\nName: {userData.firstName} {userData.lastName}\nEmail: {userData.email}\nSNN: {userData.ssn}\nPhone: {userData.phone}\nBirth Year:{userData.birthYear}");
            Console.WriteLine("\nPress ENTER to return to menu");
            Console.ReadLine();
            logon();
        }
        static void Catalog()
        {
            Console.Clear();
            int i = 0;

            if (ProdInfo.Count < 1)
            {
                Console.WriteLine("No products registred.");
                Console.ReadLine();
            }
            Console.WriteLine($"-----------------------------------");
            foreach (ProductInfo product in ProdInfo)
            {
                Console.WriteLine($"Prod Index ID:{i}");
                Console.WriteLine($"Prod Name: {product.prodName}");
                Console.WriteLine($"Prod Price: {product.prodPrice}");
                i++;
                Console.WriteLine($"-----------------------------------");

            }
            Console.WriteLine("Enter the item ID to add it to cart");
            int itID = int.Parse(Console.ReadLine());
            var prod = ProdInfo[itID];
            
            Console.WriteLine($"Select the amount:");
            int qnt = int.Parse(Console.ReadLine());
            
            Console.WriteLine($"Are you sure you want to add {qnt} {prod.prodName}?\nEnter Y to confirm or N to decline");
            var confirm = Console.ReadLine();
                if (confirm.ToUpper() != "Y")
            {
                Console.WriteLine("Purchase cancelled\n Press ENTER to return to menu");
                Console.ReadLine();
                logon();
            }
            Console.WriteLine("Product added to the cart");
            Console.ReadLine();
            var purchased = new PurchasedProductInfo();
            purchased.prodName = prod.prodName;
            purchased.prodPrice = prod.prodPrice;
            purchased.qntt = qnt;

            CartL.Add(purchased);
            
            MyCart();
        }

        static void MyCart()
        {
            
            float subtotal = 0f;
            float subtotal2 = 0f;
            var index = 0;
            Console.Clear();
            Console.WriteLine("Cart added items:");
            foreach (PurchasedProductInfo prods in CartL)
            {
                
                Console.WriteLine(prods.prodName);
                Console.WriteLine("individual Price = " + prods.prodPrice + "$\n");
                
                var total = prods.prodPrice * prods.qntt;
                index++;
                    if (index <= CartL.Count)
                {
                    subtotal += total;
                    
                }
            }
            
            Console.WriteLine("Subtotal = " + subtotal + "$");
            Console.ReadLine();

        }
    }
}
