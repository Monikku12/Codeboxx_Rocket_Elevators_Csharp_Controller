using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Column
    {
        public int ID;
        public string status;
        public int amountOfFloors;
        public int amountOfElevators;
        public List<string> elevatorsList;
        public List<string> callButtonsList;
        public List<int> servedFloorsList;
        public bool isBasement;
        // public void createElevators(int amountOfFloors, int amountOfElevators);
        // public void createElevators(int amountOfFloors, bool isBasement);

        public Column(int _id, string _status, int _amountOfFloors, int _amountOfElevators, List<int> _servedFloors, bool _isBasement)
        {
            this.ID = _id;
            this.status = _status;
            this.amountOfFloors = _amountOfFloors;
            this.amountOfElevators = _amountOfElevators;
            this.elevatorsList = new List<string>();
            this.callButtonsList = new List<string>();
            this.servedFloorsList = _servedFloors;
            this.isBasement = _isBasement;
            // this.createElevators( _amountOfFloors, _amountOfElevators);
            // this.createCallButton(_amountOfFloors, _isBasement);
            // this.requestElevator(int userPosition, string direction)
        }

        //Simulate when a user press a button on a floor to go back to the first floor
        // public Elevator requestElevator(int userPosition, string direction)
        // {
            
        // }

    }
}