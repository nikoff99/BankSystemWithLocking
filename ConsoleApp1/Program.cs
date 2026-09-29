namespace ConsoleApp1{
    internal class Program{

        public class Card {
            public int Id { get; set; }
            public string? Pan { get; set; }
            public string? Pin { get; set; }
            public string? CardCompany { get; set; }
            public string? Username { get; set; }
            public decimal Balance { get; set; }
            public void MoneyTransfer(decimal amount_money) {
                if (Balance >= amount_money) {
                    Balance = Balance - amount_money;
                    Console.WriteLine("Money Transferred Succesfully !!!");
                }
                else {
                    Console.WriteLine("Balance isn't enough !!!");
                }
            }
        }

        static object LockObject = new object();
        public static void GetMoney(object? obj) {
            if (obj is List<Card> card_list) {
                string pin;
                while (true) {
                    lock (LockObject) {
                        Console.Write("Enter your pin(4 number) :\n");
                        pin = Console.ReadLine();

                        if (pin != null) {
                            foreach (var item in card_list) {
                                if (item.Pin == pin) {
                                    Console.WriteLine("Welcome " + item.Username);

                                    Console.Write("How much money you want to transfer : ");
                                    decimal amount_of_money =
                                        Convert.ToDecimal(Console.ReadLine());

                                    item.MoneyTransfer(amount_of_money);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        static void Main(string[] args){
            Card card1 = new Card {
                Id = 1,
                Pan = "1771188119911001",
                Pin = "1234",
                CardCompany = "Visa",
                Username = "Mustafayev Ercument",
                Balance = 500
            };

            Card card2 = new Card {
                Id = 2,
                Pan = "5454646474748484",
                Pin = "5678",
                CardCompany = "MasterCard",
                Username = "Leo Messi",
                Balance = 1200
            };

            Card card3 = new Card {
                Id = 3,
                Pan = "19283746556171",
                Pin = "7156",
                CardCompany = "MasterCard",
                Username = "Qiyas Qiyasov",
                Balance = 500
            };

            List<Card> cards = new List<Card>();
            cards.Add(card1);
            cards.Add(card2);
            cards.Add(card3);

            Thread th1 = new Thread(GetMoney);
            th1.Start(cards);
            Thread th2 = new Thread(GetMoney);
            th2.Start(cards);
        }
    }
}
