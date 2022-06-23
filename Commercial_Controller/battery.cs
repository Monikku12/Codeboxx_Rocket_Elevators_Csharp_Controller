using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Battery
    {
        public int ID, _amountOfElevators, _currentFloor;
        public string status;
        public Column column;
        public Elevator elevator;
        public bool _isBasement;
        public List<Column> columnsList;
        public List<FloorRequestButton> floorRequestsButtonsList;

        public Battery(int _id, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevators)
        {
            this.ID = _id;
            this.status = "online";
            this.columnsList = new List<Column>();
            this.floorRequestsButtonsList = new List<FloorRequestButton>();
            // this.servedFloorsList = new List<int>();
            // this.column = new Column(ID, status, _amountOfFloors, servedFloorsList, _amountOfElevators, _isBasement);
            // this.elevator = new Elevator(_id, status, _amountOfFloors, _currentFloor);

            if (_amountOfBasements > 0)
            {
                this.createBasementFloorRequestButtons(_amountOfBasements);
                this.createBasementColumn(_amountOfBasements, _amountOfElevators);
                _amountOfColumns--;
            }
            this.createFloorRequestButtons(_amountOfFloors);
            this.createColumns(_amountOfColumns, _amountOfFloors, _amountOfBasements, _amountOfElevators);
            // this.findBestColumn(_requestedFloor);

        }



        public void createBasementColumn(int _amountOfBasements, int _amountOfElevatorsPerColumn)
        {
            List<int> servedBasementFloors = new List<int>();
            int floor = -1;
            for (int i = 0; i < _amountOfBasements; i++)
            {
                servedBasementFloors.Add(floor);
                floor--;
            }
            Column column = new Column(ID, "online", _amountOfBasements, servedBasementFloors, _amountOfElevators, true);
            this.columnsList.Add(column);
        }

        public void createColumns(int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevators)
        {
            double d = _amountOfFloors / _amountOfColumns;
            int amountOfFloorsPerColumn = (int)Math.Ceiling(d);
            int floor = 1;
            for (int i = 0; i < _amountOfColumns; i++)
            {

                List<int> servedFloors = new List<int>();
                for (int a = 0; a < amountOfFloorsPerColumn; a++)
                {
                    if (floor <= _amountOfFloors)
                    {
                        servedFloors.Add(floor);
                        floor++;
                    }
                }

                Column column = new Column(i, "online", _amountOfFloors, servedFloors, _amountOfElevators, false);
                this.columnsList.Add(column);
                // i++;
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
            Column bestColumn = columnsList[0];

            foreach (Column column in this.columnsList)
            {
                if (column.servedFloorsList.Contains(_requestedFloor))
                {
                    bestColumn = column;
                }
            }


            return bestColumn;
        }

        //Simulate when a user press a button at the lobby
        public (Column, Elevator) assignElevator(int _requestedFloor, string direction)
        {
            Column column = this.findBestColumn(_requestedFloor);
            Elevator elevator = column.findElevator(1, direction); // The floor is always 1 because that request is always made from the lobby.
            elevator.addNewRequest(1);
            elevator.move();
            elevator.addNewRequest(_requestedFloor);
            elevator.move();
            return (column, elevator);
        }
    }
}