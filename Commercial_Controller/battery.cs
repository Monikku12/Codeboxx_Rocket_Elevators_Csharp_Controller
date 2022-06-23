using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Battery
    { 
    public int ID;
    public string status;
    public int _requestedFloor;
    public int _amountOfElevators;
    public Column column;
    public List<Column> columnsList;
    public List<FloorRequestButton> floorRequestsButtonsList;
    public List<int> servedFloorsList;
 
    public Battery(int _id, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorsPerColumn)
        {             
            this.ID = _id;
            this.status = "online";
            this.columnsList = new List<Column>();
            this.floorRequestsButtonsList = new List<FloorRequestButton>();
            this.servedFloorsList = new List<int>();
            
            if (_amountOfBasements > 0)
            {
                this.createBasementFloorRequestButtons(_amountOfBasements);
                this.createBasementColumn(_amountOfBasements, _amountOfElevatorsPerColumn);
                _amountOfColumns--;
            }
            this.createFloorRequestButtons(_amountOfFloors);
            this.createColumns(_amountOfColumns, _amountOfFloors, _amountOfBasements, _amountOfElevatorsPerColumn);
            this.findBestColumn(_requestedFloor);

        }



        public void createBasementColumn(int _amountOfBasements, int _amountOfElevatorsPerColumn)
        {
            this.servedFloorsList = new List<int>();
            int floor = -1;
            for (int i = 0; i < _amountOfBasements; i++)
            {
                this.servedFloorsList.Add(floor);
                floor--;
            }
                Column column = new Column(ID, "online", _amountOfBasements, this.servedFloorsList, _amountOfElevators, true);
                this.columnsList.Add(column);
        }

        public void createColumns(int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            double d = _amountOfFloors / _amountOfColumns;
            int amountOfElevatorPerColumn = (int)Math.Ceiling(d);
            int floor = 1;
            for (int i = 0; i < _amountOfColumns; i++)
            {
                this.columnsList = new List<Column>();
                for (int a = 0; a < _amountOfElevatorPerColumn; a++)
                {
                    if (floor <= _amountOfFloors)
                    {
                        this.servedFloorsList.Add(floor);
                        floor++;
                    }
                Column column = new Column(i + 1, "online", _amountOfBasements, this.servedFloorsList, _amountOfElevators, false);
                this.columnsList.Add(column);
                }
            }
        }


        public void createFloorRequestButtons(int _amountOfFloors)
        {
            int buttonFloor = 1;
            for (int i = 0; i < _amountOfFloors; i++)
            {
                FloorRequestButton floorRequestButton = new FloorRequestButton(i + 1, "OFF", buttonFloor, "Up");
                this.floorRequestsButtonsList.Add(floorRequestButton);
                buttonFloor++;
            }
        }

        public void createBasementFloorRequestButtons(int _amountOfBasements)
        {
            int buttonFloor = -1;
            for (int i = 0; i < _amountOfBasements; i++)
            {
                FloorRequestButton floorRequestButton = new FloorRequestButton(i + 1, "OFF", buttonFloor, "Down");
                this.floorRequestsButtonsList.Add(floorRequestButton);
                buttonFloor--;
            }
        }

        public Column findBestColumn(int _requestedFloor)
        {
            foreach (Column column in this.columnsList)
            {
                if (this.servedFloorsList.Contains(_requestedFloor))
                {
                    return column;
                }
            }
            return column; 
        }

        //Simulate when a user press a button at the lobby
        public (Column, Elevator) assignElevator(int _requestedFloor, string requestedDirection)
        {
            Column column = this.findBestColumn(_requestedFloor);
            Elevator elevator = column.findElevator(1, requestedDirection); // The floor is always 1 because that request is always made from the lobby.
            elevator.addNewRequest(1);
            elevator.move();
            elevator.addNewRequest(_requestedFloor);
            elevator.move();
            return (column, elevator);
        }
    }
}