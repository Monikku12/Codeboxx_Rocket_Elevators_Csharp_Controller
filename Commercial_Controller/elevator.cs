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
        public Elevator(int _id, string _status, int _amountOfFloors, int _currentFloor, int _elevatorID)
        {
            this.ID = _id;
            this.status = _status;
            this.amountOfFloors = _amountOfFloors;
            this.currentFloor = _currentFloor;
            this.door = new Door(_id, "closed");
            this.floorRequestList = new List<int>();
            this.direction = null;
            this.overweight = false;
            {
                
            }

        }
        public void move()
        {

        }
        
    }
}