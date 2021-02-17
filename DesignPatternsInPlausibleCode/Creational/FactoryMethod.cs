using System;
using System.Collections.Generic;

namespace DesignPatternsInPlausibleCode.Creational
{
    // example modified from https://en.wikipedia.org/wiki/Factory_method_pattern
    // each class will get a comment, indicating what class it corresponds to in the UML class diagram of the pattern

    public static class FactoryMethod
    {
        public static void Run()
        {
            var ordinaryMazegame = new OrdinaryMazeGame();
            ordinaryMazegame.Play();

            var magicMazegame = new MagicMazeGame();
            magicMazegame.Play();
        }
    }
    
    // acts as the Product
    abstract class Room
    {
        protected string name;
        private readonly List<Room> connectedRooms = new List<Room>();

        public void Connect(Room room)
        {
            connectedRooms.Add(room);
        }

        public void EnterRoom()
        {
            Console.WriteLine("In " + name);

            foreach (var connectedRoom in connectedRooms)
            {
                connectedRoom.EnterRoom();
            }
        }
    }

    // acts as the ConcreteProduct1
    class OrdinaryRoom : Room
    {
        public OrdinaryRoom(int number)
        {
            name = "Ordinary Room " + number;
        }
    }

    // acts as the ConcreteProduct2
    class MagicRoom : Room
    {
        public MagicRoom(int number)
        {
            name = "Magic Room " + number;
        }
    }

    // acts as the Creator
    abstract class MazeGame
    {
        private readonly Room startingRoom;

        public MazeGame()
        {
            Room room1 = MakeRoom(1);
            Room room2 = MakeRoom(2);
            Room room3 = MakeRoom(3);

            room1.Connect(room2);
            room2.Connect(room3);

            startingRoom = room1;
        }

        // THE factory method
        abstract protected Room MakeRoom(int number);

        public void Play()
        {
            startingRoom.EnterRoom();
        }
    }

    // acts as the ConcreteCreator1
    class OrdinaryMazeGame : MazeGame
    {
        protected override Room MakeRoom(int number)
        {
            return new OrdinaryRoom(number);
        }
    }

    // acts as the ConcreteCreator2
    class MagicMazeGame : MazeGame
    {
        protected override Room MakeRoom(int number)
        {
            return new MagicRoom(number);
        }
    }
}
