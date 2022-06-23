using System.Threading;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Elevator
    {
        public int ID, currentFloor, screenDisplay;
        public string status, direction;
        public int amountOfFloors;
        public Door door;
        public bool overweight, obstruction;
        public List<int> completedRequestsList, floorRequestsList;
        public Elevator(int _id, string _status, int _amountOfFloors, int _currentFloor)
        {
            this.ID = _id;
            this.status = _status;
            this.amountOfFloors = _amountOfFloors;
            this.currentFloor = _currentFloor;
            this.door = new Door(_id, "closed");
            this.floorRequestsList = new List<int>();
            this.direction = null;
            this.overweight = false;
            this.obstruction = false;
            this.screenDisplay = 1;
            this.completedRequestsList = new List<int>();
        }
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
                    this.screenDisplay = this.currentFloor;
                   }
                }
                else if (this.currentFloor > destination)
                {
                    this.direction = "down";
                    this.sortFloorList();
                    while (this.currentFloor < destination)
                    {
                        this.currentFloor--;
                        this.screenDisplay = this.currentFloor;
                    }
                }
                this.status = "stopped";
                this.operateDoors();
                this.completedRequestsList.Add(this.floorRequestsList[0]);
                this.floorRequestsList.RemoveAt(0);                
            }
            this.status = "idle";
        }
        
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

        public void operateDoors()
        {
            this.door.status = "opened";
            // Console.WriteLine("Wait 5 seconds");
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
                    // Console.WriteLine("Activate overweight alarm")
                }
                this.operateDoors();
            }
        }

        public void addNewRequest(int requestedFloor)
        {
        if (this.floorRequestsList.Count != requestedFloor)
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