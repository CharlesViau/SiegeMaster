using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Types;
using System;
using General;
using UnityEngine.AI;
using System.Linq;

public class puQUEST : MonoBehaviour
{
    PlayerUnit player;
    NavMeshAgent navMeshAgent;
    public Body balls;
  //  int numberOfFrame = 0;

    int i = 0;

   // int numberOfList = 0;
   // int numberOfBatchFrame = 0;
    int numberOfBatchFrameSave = 0;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerUnit>();
        balls.StartBD(balls);
     //   numberOfBatchFrameSave = balls.partsList.Count /15;
     //   numberOfBatchFrame = numberOfBatchFrameSave;
    }
    public void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);

        //Batching 

        //if (numberOfBatchFrame >= balls.partsList.Count)
        //{

        //    numberOfBatchFrame = balls.partsList.Count;
        //}

        //for (i = numberOfList; i < numberOfBatchFrame; i++)
        //{

        //    balls.partsList[i].ForceCalculation();
        //}
        //numberOfList += numberOfBatchFrame;
        //numberOfBatchFrame += numberOfBatchFrameSave;

        //if (numberOfList > balls.partsList.Count)
        //{
        //    numberOfList = 0;

        //}


     //   balls.RefreshBD(balls);

    }

    [Serializable]
    public class Body
    {
        bool isAlive = true;
        public string partName;
        public Transform centerOfCell;
        public int numberOfCells;
        public int numberOfDesireCells;
        public List<Body> children = new List<Body>();
        public HashSet<Cell> partsList = new HashSet<Cell>();


       public  Stack<Cell> toRemove=new Stack<Cell>();

        public void StartBD(Body bd)
        {
            for (int i = 0; i < bd.children.Count; i++)
            {
                for (int j = 0; j < bd.children[i].numberOfCells; j++)
                {
                    var v = CellManager.Instance.Create(CellType.Normal, new Cell.Args(bd.children[i].centerOfCell.position, bd.children[i].centerOfCell, bd.children[i]));
                    bd.children[i].partsList.Add(v);
                    v.gameObject.name = UnityEngine.Random.Range(0, 9999).ToString(); 
                }
                bd.children[i].numberOfDesireCells = bd.children[i].numberOfCells;
                StartBD(bd.children[i]);
            }

        }


        public void DonateCell(Cell cell,Body newBody)
        {
            cell.body.partsList.Remove(cell);
            cell.body = newBody;
            cell.body.partsList.Add(cell);
            cell.cellPosition = newBody.centerOfCell;
        }

        public void CellDeath(Cell cell)
        {
            partsList.Remove(cell);
            if (CheckIfIhaveCellInChild(this))
            {
                int rand = UnityEngine.Random.Range(0, this.children.Count);
                if (cell.body.children[rand].partsList.Count >0)
                {
                    cell.body.children[rand].DonateCell(cell.body.children[rand].partsList.First(), cell.body);
                }
            }   
        }




        //public void RefreshBD(Body bd)
        //{
        //    for (int i = 0; i < bd.children.Count; i++)
        //    {
        //        for (int j = 0; j < bd.children[i].numberOfCells; j++)
        //        {
        //         //   Debug.Log(bd.children[i].numberOfCells);
        //            if (bd.children[i].numberOfCells < bd.children[i].numberOfDesireCells && CheckIfIhaveCellInChild(bd.children[i]))
        //            {


        //                Cell cell;
        //                cell = bd.children[0].partsList[0];                 
        //                toRemove.Push(cell);
        //               // bd.children[rand].partsList.Remove(cell);

        //                cell.body = bd.children[i];
        //                cell.cellPosition = bd.children[i].centerOfCell;
        //            }
        //        }
        //        RefreshBD(bd.children[i]);

        //    }

        //    foreach (Cell item in toRemove)
        //    {
        //        item.body.partsList.Remove(item);
        //    }
        //    toRemove.Clear();
        //}
        public bool CheckIfIhaveCellInChild(Body bd)
        {
            bool findCell = false;
            if (bd.children.Count>0)
            {
                for (int i = 0; i < bd.children.Count; i++)
                {
                    if (bd.children[i].partsList.Count > 0)
                    {
                        return  true;
                        
                    }
                }
            }
            return findCell;

        }


    }
}
