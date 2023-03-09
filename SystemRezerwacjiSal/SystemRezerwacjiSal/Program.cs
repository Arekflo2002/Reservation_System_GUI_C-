


namespace SystemRezerwacjiSal

{
    class Program
    {
        static void Main()
        {
            // Creating clients 
            Client client1 = new("12345678910", "John", "Smith", "123456789", "Yes");
            Client client2 = new("10987654321", "Fred", "Clown", "987654321", "No");

            // Creating rooms
            Room room1 = new("Yes", "Yes", "Yes", 30, "Yes", "Yes", 1, 25);
            Room room2 = new("No", "Yes", "No", 90, "Yes", "No", 2, 35);
            Room room3 = new("yes", "No", "No", 15, "No", "no", 5, 60);
            Room room4 = new("No", "Yes", "Yes", 300, "Yes", "No", 100, 500);

            // Creating Buildings 
            Building building2 = new("Witolda budryka 3", "34-615", "Kear");
            building2.Adding_room(room4);
            building2.Adding_room(room2);
            Building building1 = new("Jozefa Rostafinskiego 6", "30-062", "Polska");
            building1.Adding_room(room1);
            building1.Adding_room(room3);
            Building building3 = new("Aleje Mickiewicza 3", "30-982", "Niemcy");
            building3.Adding_room(room1);
            building3.Adding_room(room2);
            building3.Adding_room(room3);
            building3.Adding_room(room4);


            // Creating Reservations 
            Reservation reservation1 = new(client1, building1, room1, "23-02-2023");
            Reservation reservation2 = new(client2, building3, room3, "24-02-2023");
            Reservation reservation3 = new(client2, building2, room2, "25-02-2023");
            Reservation reservation4 = new(client1, building3, room2, "25-02-2023");
            Reservation reservation5 = new(client2, building2, room2, "28-02-2023");




            // Creating Database
            DataBase dataBase = new();
            dataBase.Adding_building(building1);
            dataBase.Adding_building(building2);
            dataBase.Adding_building(building3);
            dataBase.Adding_client(client1);
            dataBase.Adding_client(client2);
            dataBase.Adding_reservation(reservation1);
            dataBase.Adding_reservation(reservation2);
            dataBase.Adding_reservation(reservation3);
            dataBase.Adding_reservation(reservation4);
            dataBase.Adding_reservation(reservation5);


            // Saving to XML 
            string fname = "SystemForReservation.xml";
            dataBase.SaveXML(fname);
            

        }
    }
}
