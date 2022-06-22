using System.Threading;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Elevator
    {
        public int ID;
        public string status;
        public int amountOfFloors;
        public int currentFloor;
        public Door door;
        public List<int> floorRequestList;
        public string direction;
        public bool overweight;
        public bool obstruction;
        public int screenDisplay;
        public Elevator(int _id, string _status, int _amountOfFloors, int _currentFloor)
        {
            this.ID = 1;
            this.status = _status;
            this.amountOfFloors = _amountOfFloors;
            this.currentFloor = _currentFloor;
            this.door = new Door(_id, "closed");
            this.floorRequestList = new List<int>();
            this.direction = null;
            this.overweight = false;
            this.obstruction = false;
            this.screenDisplay = 1;
        }
        public void move()
        {
        while (this.floorRequestList.Count != 0)
            {
                int destination = this.floorRequestList[0];
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
                this.floorRequestList.RemoveAt(0);                
            }
            this.status = "idle";
        }
        
        public void sortFloorList()
        {
            if (this.direction == "up")
            {
               this.floorRequestList.Sort();
            }
            else
            {
                this.floorRequestList.Reverse();
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
        if (this.floorRequestList.Count != requestedFloor)
            {
                this.floorRequestList.Add(requestedFloor);
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