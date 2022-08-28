using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// ��������� ��������� ���������� ������
public class Maze 
{
    public float borderChance=0.5f;
    public MazeCell[][] cells;
    int columnCount;
    int rowCount;
    int counter;

    public Maze(int columnCount, int rowCount)
    {
        this.columnCount = columnCount;
        this.rowCount = rowCount;

        cells = new MazeCell[rowCount][];
        for (int i = 0; i < rowCount; i++)
        {
            cells[i] = new MazeCell[columnCount];
            for (int j = 0; j < cells[i].Length; j++)
            {
                cells[i][j] = new MazeCell();
            }
        }

        Generate();
    }

    void Generate()
    {
        FillFirstRow( cells[0]);
        for (int i = 1; i < rowCount-1; i++)
        {
            FillRow( cells[i - 1], cells[i]);
        }
        FillLastRow( cells[rowCount - 2],  cells[rowCount - 1]);

        // ������� �� ��������� ���������
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i][0].leftBorder = true;
            cells[i][columnCount - 1].rightBorder = true;
        }
        for (int j = 0; j < cells[0].Length; j++)
        {
            cells[0][j].topBorder = true;
            cells[rowCount - 1][j].bottomBorder = true;
        }
    }

    void FillFirstRow( MazeCell[] firstRow)
    {
        for (int i = 0; i < firstRow.Length; i++)
        {
            firstRow[i].value = i+1;
        }
        counter = rowCount+1;
        CreateRandomRightBorders( firstRow);
        CreateRandomBottomBorders( firstRow);
    }

    void FillRow( MazeCell[] previousRow,  MazeCell[] row)
    {
        // �������� ���������� ������
        for (int i = 0; i < row.Length; i++)
        {
            row[i].value = 0;
            if (!previousRow[i].bottomBorder)
            {
                row[i].value = previousRow[i].value;
            }
        }

        for (int i = 0; i < row.Length; i++)
        {
            // �������� ������� ������� ������ �������
            // ����������� ��� ������ ����������
            if (row[i].value ==0)
            {
                row[i].value = counter++;
            }
        }
        CreateRandomRightBorders( row);
        CreateRandomBottomBorders( row);
    }

    void FillLastRow( MazeCell[] previousRow, MazeCell[] lastRow)
    {
        // ��������� ��� ������� ������
        FillRow( previousRow,  lastRow);

        // ������� ������ ������� � ������ ��������
        for (int i = 0; i < lastRow.Length-1; i++)
        {
            if (lastRow[i].value != lastRow[i+1].value)
            {
                lastRow[i].rightBorder = false;
            }
        }
    }

    void CreateRandomRightBorders( MazeCell[] row)
    {
        for (int i = 0; i < row.Length-1; i++)
        {
            // ���� ������� ������ � ������ ������ ����������� ������ ���������,
            // �� ������� ������� ����� ���� (��� �������������� ������������)
            if (row[i + 1].value == row[i].value)
            {
                row[i].rightBorder = true;
            }
            else if (Random.value < borderChance)
            {
                row[i].rightBorder = true;
            }
            else
            {
                // ���� ������� �� �������, ���������� �������
                row[i + 1].value = row[i].value;
            }
        }
    }

    void CreateRandomBottomBorders( MazeCell[] row)
    {
        for (int i = 0; i < row.Length; i++)
        {
            // ������� ���������� ����� � �������
            int cellsCount = 0;
            int bordersCount = 0;
            for (int j = 0; j < row.Length; j++)
            {
                if (row[i].value == row[j].value)
                {
                    cellsCount++;
                    if (row[j].bottomBorder)
                    {
                        bordersCount++;
                    }
                }
            }

            for (int j = 0; j < row.Length; j++)
            {
                if (row[i].value == row[j].value)
                {
                    // ���� �� ���� ������ ������ ���� ��� ������ �������
                    // (��� �������������� ������������ ��������)
                    if (bordersCount == cellsCount - 1)
                    {
                        break;
                    }
                    if (Random.value > borderChance)
                    {
                        bordersCount++;
                        row[j].bottomBorder = true;
                    }
                }
            }
        }
    }
    
}
