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
 
    // public void IF (int amountOfBasements > 0);    
    // public void createFloorRequestButtons(int amountOfFloors);
    // public void createColumns(int amountOfColumns, int amountOfFloors, int amountOfElevatorPerColumn);
    // public void createBasementFloorRequestButtons(int amountOfBasements);
    // public void createBasementColumn(int amountOfBasements, int amountOfElevatorPerColumn);



    public Battery(int _id, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {             
            this.ID = _id;
            this.status = "online";
            this.columnsList = new List<Column>();
            this.floorRequestsButtonsList = new List<FloorRequestButton>();
    
//             IF (_amountOfBasements > 0);
//             {
//                 createBasementFloorRequestButtons(_amountOfBasements);
//                 createBasementColumn(_amountOfBasements, _amountOfElevatorPerColumn);
//                 _amountOfColumns--;
//             }
//             createFloorRequestButtons(_amountOfFloors);
//             createColumns(_amountOfColumns, _amountOfFloors, _amountOfElevatorPerColumn);

        }



        public 

        public Column findBestColumn(int _requestedFloor)
        {
            foreach (column in this.columnsList)
            {
                if (column.servedFloorList.contains(_requestedFloor))
                {
                    return column;
                }
            }
        }

        //Simulate when a user press a button at the lobby
        public (Column, Elevator) assignElevator(int _requestedFloor, string requestedDirection)
        {
            column = this.findBestColumn(_requestedFloor);
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

