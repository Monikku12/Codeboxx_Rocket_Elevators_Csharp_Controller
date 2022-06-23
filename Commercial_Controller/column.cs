using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Column
    {
        public int ID, amountOfFloors, amountOfElevators;
        public string status;
        public List<Elevator> elevatorsList;
        public List<CallButton> callButtonsList;
        public List<int> servedFloorsList;
        public bool isBasement;

        public Column(int _id, string _status, int _amountOfFloors, List<int> servedFloorsList, int _amountOfElevators, bool _isBasement)
        {
            this.ID = _id;
            this.status = _status;
            this.servedFloorsList = servedFloorsList;
            this.amountOfFloors = _amountOfFloors;
            this.amountOfElevators = _amountOfElevators;
            this.elevatorsList = new List<Elevator>();
            this.callButtonsList = new List<CallButton>();
            this.isBasement = _isBasement;
            this.createElevators(_amountOfFloors, _amountOfElevators);
            this.createCallButtons(_amountOfFloors, _isBasement);
        }

        public void createCallButtons(int _amountOfFloors, bool _isBasement)
        {
            if (_isBasement)
            {
                int buttonFloor = -1;
                for (int i = 0; i < _amountOfFloors; i++)
                {
                    CallButton callButton = new CallButton(i + 1, "OFF", buttonFloor, "Up");
                    this.callButtonsList.Add(callButton);
                    buttonFloor--;
                }
            }
            else
            {
                int buttonFloor = 1;
                for (int i = 0; i < _amountOfFloors; i++)
                {
                    CallButton callButton = new CallButton(i + 1, "OFF", buttonFloor, "Down");
                    this.callButtonsList.Add(callButton);
                    buttonFloor++;
                }
            }
        }

        public void createElevators(int _amountOfFloors, int _amountOfElevators)
        {
            for (int i = 0; i < _amountOfElevators; i++)
            {
                Elevator elevator = new Elevator(i + 1, "idle", _amountOfFloors, 1);
                this.elevatorsList.Add(elevator);
            }
        }



        // Simulate when a user press a button on a floor to go back to the first floor
        public Elevator requestElevator(int userPosition, string direction)
        {
            Elevator elevator = this.findElevator(userPosition, direction);
            elevator.addNewRequest(userPosition);
            elevator.move();
            elevator.addNewRequest(1); //Always 1 because the user can only go back to the lobby
            elevator.move();
            return elevator;
        }

        //We use a score system depending on the current elevators state. Since the bestScore and the referenceGap are
        //higher values than what could be possibly calculated, the first elevator will always become the default bestElevator,
        //before being compared with to other elevators. If two elevators get the same score, the nearest one is prioritized. Unlike
        //the classic algorithm, the logic isn't exactly the same depending on if the request is done in the lobby or on a floor.
        public Elevator findElevator(int requestedFloor, string requestedDirection)
        {
            BestElevatorInformations bestElevatorInformations = new BestElevatorInformations();

            if (requestedFloor == 1)
            {
                foreach (Elevator elevator in this.elevatorsList)
                {
                    // The elevator is at the lobby and already has some requests. It is about to leave but has not yet departed.
                    if (1 == elevator.currentFloor && elevator.status == "stopped")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(1, elevator, bestElevatorInformations, requestedFloor);
                    }
                    // The elevator is at the lobby and has no requests
                    else if (1 == elevator.currentFloor && elevator.status == "idle")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(2, elevator, bestElevatorInformations, requestedFloor);
                    }
                    // The elevator is lower than me and is coming up. It means that I'm requesting an elevator to go to a basement, and the elevator is on it's way to me.
                    else if (1 > elevator.currentFloor && elevator.direction == "up")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(3, elevator, bestElevatorInformations, requestedFloor);
                    }
                    // The elevator is above me and is coming down. It means that I'm requesting an elevator to go to a floor, and the elevator is on it's way to me
                    else if (1 < elevator.currentFloor && elevator.direction == "down")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(3, elevator, bestElevatorInformations, requestedFloor);
                    }
                    // The elevator is not at the first floor, but doesn't have any request
                    else if (elevator.status == "idle")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(4, elevator, bestElevatorInformations, requestedFloor);
                    }
                    // The elevator is not available, but still could take the call if nothing better is found
                    else
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(5, elevator, bestElevatorInformations, requestedFloor);
                    }
                }
            }
            else
            {
                foreach (Elevator elevator in this.elevatorsList)
                {
                    // The elevator is at the same level as me, and is about to depart to the first floor
                    if (requestedFloor == elevator.currentFloor && elevator.status == "stopped" && requestedDirection == elevator.direction)
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(1, elevator, bestElevatorInformations, requestedFloor);
                    }
                    // The elevator is lower than me and is going up. I'm on a basement, and the elevator can pick me up on it's way
                    else if (requestedFloor > elevator.currentFloor && elevator.direction == "up" && requestedDirection == "up")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(2, elevator, bestElevatorInformations, requestedFloor);
                    }
                    // The elevator is higher than me and is going down. I'm on a floor, and the elevator can pick me up on it's way
                    else if (requestedFloor < elevator.currentFloor && elevator.direction == "down" && requestedDirection == "down")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(2, elevator, bestElevatorInformations, requestedFloor);
                    }
                    // The elevator is idle and has no requests
                    else if (elevator.status == "idle")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(4, elevator, bestElevatorInformations, requestedFloor);
                    }
                    // The elevator is not available, but still could take the call if nothing better is found
                    else
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(5, elevator, bestElevatorInformations, requestedFloor);
                    }
                }
            }
            return bestElevatorInformations.bestElevator;
        }




        public BestElevatorInformations checkIfElevatorIsBetter(int scoreToCheck, Elevator newElevator, BestElevatorInformations bestElevatorInformations, int floor)
        {
            if (scoreToCheck < bestElevatorInformations.bestScore)
            {
                bestElevatorInformations.bestScore = scoreToCheck;
                bestElevatorInformations.bestElevator = newElevator;
                bestElevatorInformations.referenceGap = Math.Abs(newElevator.currentFloor - floor);
            }
            else if (bestElevatorInformations.bestScore == scoreToCheck)
            {
                int gap = Math.Abs(newElevator.currentFloor - floor);
                if (bestElevatorInformations.referenceGap > gap)
                {
                    bestElevatorInformations.bestElevator = newElevator;
                    bestElevatorInformations.referenceGap = gap;
                }
            }
            return bestElevatorInformations;
        }
    }
    public class BestElevatorInformations
    {
        public Elevator bestElevator;
        public int bestScore;
        public int referenceGap;

        public BestElevatorInformations()
        {
            this.bestElevator = null;
            this.bestScore = 6;
            this.referenceGap = 10000000;
        }
    }
}