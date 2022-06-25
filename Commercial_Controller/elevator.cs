using System;
using System.Threading;
using System.Collections.Generic;

namespace Commercial_Controller
{
    // Elevators in a column
    public class Elevator
    {
        public int ID, currentFloor, amountOfFloors;
        public string status, direction;
        public Door door;
        public bool overweight, obstruction;
        public List<int> completedRequestsList, floorRequestsList;
        public Elevator(int _id, string _status, int _amountOfFloors, int _currentFloor)
        {
            this.ID = _id;
            this.status = "idle";
            this.amountOfFloors = _amountOfFloors;
            this.currentFloor = _currentFloor;
            this.door = new Door(_id, "closed");
            this.floorRequestsList = new List<int>();
            this.completedRequestsList = new List<int>();
        }

        // Function in charge of moving the elevator in the columns
        public void move()
        {
        while (this.floorRequestsList.Count != 0)
            {
                int destination = this.floorRequestsList[0];
                this.status = "moving";
                if (this.currentFloor < destination)
                {
                   this.direction = "up";
                   this.sortFloorList();
                   while (this.currentFloor < destination)
                   {
                    this.currentFloor++;
                    int screenDisplay = this.currentFloor;
                   }
                }
                else if (this.currentFloor > destination)
                {
                    this.direction = "down";
                    this.sortFloorList();
                    while (this.currentFloor > destination)
                    {
                        this.currentFloor--;
                        int screenDisplay = this.currentFloor;
                    }
                }
                this.status = "stopped";
                this.operateDoors();
                this.completedRequestsList.Add(this.floorRequestsList[0]);
                this.floorRequestsList.RemoveAt(0);                
            }
            this.status = "idle";
        }
        
        // Function in charge of sorting the floors in the right order depending if the elevator is going up or down
        public void sortFloorList()
        {
            if (this.direction == "up")
            {
               this.floorRequestsList.Sort();
            }
            else
            {
                this.floorRequestsList.Reverse();
            }
        }

        // Function in charge of opening and closing the elevator doors
        public void operateDoors()
        {
            this.door.status = "opened";
            Console.WriteLine("Wait 5 seconds");
            if (this.overweight == false)
            {
                this.door.status = "closing";
                if (this.obstruction == false)
                {
                    this.door.status = "closed";
                }
                else
                {
                    this.operateDoors();
                }
            }
            else
            {
                while (this.overweight == true)
                {
                Console.WriteLine("Activate overweight alarm");
                }
                this.operateDoors();
            }
        }

        // Function in charge of adding the requested floor to the Move function so that it send the elevator to the right floor
        public void addNewRequest(int requestedFloor)
        {
        if (this.floorRequestsList.Contains(requestedFloor) == false)
            {
                this.floorRequestsList.Add(requestedFloor);
            }
        if (this.currentFloor < requestedFloor)
            {
                this.direction = "up";
            }
        if (this.currentFloor > requestedFloor)
            {
                this.direction = "down";
            }
        }
    }
}