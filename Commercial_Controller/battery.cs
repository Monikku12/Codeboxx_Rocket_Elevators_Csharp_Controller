using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Battery
    { 
    public int ID;
    public string status;
    public List<Column> columnsList;
    public List<FloorRequestButton> floorRequestsButtonsList;
 
    public Battery(int _id, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {             
            this.ID = _id;
            this.status = "online";
            this.columnsList = new List<Column>();
            this.floorRequestsButtonsList = new List<FloorRequestButton>();
            this.createFloorRequestButtons(_amountOfFloors);
            this.createColumns(_amountOfColumns, _amountOfFloors, _amountOfElevatorPerColumn);
            this.createBasementFloorRequestButtons(_amountOfBasements);
            this.createBasementColumn(_amountOfBasements, _amountOfElevatorPerColumn);

//             IF (_amountOfBasements > 0);
//             {
//                 createBasementFloorRequestButtons(_amountOfBasements);
//                 createBasementColumn(_amountOfBasements, _amountOfElevatorPerColumn);
//                 _amountOfColumns--;
//             }
//             createFloorRequestButtons(_amountOfFloors);
//             createColumns(_amountOfColumns, _amountOfFloors, _amountOfElevatorPerColumn);

        }



        public void createBasementColumn(int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            Column.servedFloorsList = null;
            int floor = -1;
            for (int i = 0; i < _amountOfBasements; i++)
            {
                Column column = new Column(i + 1, "online", _amountOfBasements, _amountOfElevatorsPerColumn, servedFloors, true);
                this.columnsList.Add(column);
                Column.servedFloorsList.Add(floor);
                floor--;
            }
        }

        public void createColumns(int _amountOfColumns, int _amountOfFloors, int _amountOfElevatorPerColumn)
        {
            _amountOfElevatorPerColumn = Math.Ceiling(_amountOfFloors / _amountOfColumns);
            int floor = 1;
            for (int i = 0; i < _amountOfColumns; i++)
            {
                Column.servedFloorsList = null;
                for (int i = 0; i < _amountOfElevatorPerColumn; i++)
                {
                    if (floor <= _amountOfFloors)
                    {
                        Column.servedFloorsList.Add(floor);
                        floor++;
                    }
                Column column = new Column(i + 1, "online", _amountOfFloors, _amountOfElevators, servedFloorsList, false);
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

        public Column Elevator.findBestColumn(int _requestedFloor)
        {
            foreach (column in this.columnsList)
            {
                if (Column.servedFloorsList.contains(_requestedFloor))
                {
                    return column;
                }
            }
        }

        //Simulate when a user press a button at the lobby
        public (Column, Elevator) assignElevator(int _requestedFloor, string requestedDirection)
        {
            Column column = this.findBestColumn(_requestedFloor);
            Elevator elevator = Column.findElevator(1, requestedDirection); // The floor is always 1 because that request is always made from the lobby.
            elevator.addNewRequest(1);
            elevator.move();
            elevator.addNewRequest(_requestedFloor);
            elevator.move();
            return column;
            return bestElevator;
        }
    }
}

