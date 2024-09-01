#include <iostream>
#include <string>
#include "Graph.h"
#include "MC.h"

using std::cout;
using std::endl;
using std::vector;
using std::stoi;


//Function Headers
Graph<int> populateNumbersGraph();
bool testWithNumbers();
bool testMissionariesCannibals();
bool checkCase(string name, bool condition);

int main(int argc, char** argv){

   int test = 0;
   int count = 0;

   if(argc > 1){
      test = stoi(argv[1]);
   }
   switch(test){
      case 0:
         if (testWithNumbers()) count++;
         if (testMissionariesCannibals()) count++;
         cout << "Passed " << count << " out of 2 tests";
         return count != 2;
      case 1:
         return !testWithNumbers();
      case 2:
         return !testMissionariesCannibals();
   }
}

bool testWithNumbers(){
  bool passed = true;
  cout << "Numbers Graph" << endl;
  Graph<int> g = populateNumbersGraph();

  cout << g << endl;
  cout << "\nNumber Graph Test Cases" << endl;
  passed &= checkCase("Numbers Test size", g.getNumVertices() == 10);
  passed &= checkCase("Vertex position test", g.findVertexPos(7) == 3);

  vector<int>sol = g.getPath(0, 5);
  passed &= checkCase("Solution implemented", sol.size() > 0);
  if(passed){
    passed &= checkCase("Checking path order 1", sol[0] == 0);
    passed &= checkCase("Checking path order 2", sol[sol.size() - 1] == 5);
    passed &= checkCase("Checking solution size", sol.size() <= 4);

    int possPath1 [] = {0, 1, 4, 5};
    int possPath2 [] = {0, 3, 6, 5};

    bool isPath = true;
    for(int i = 0; i < sol.size(); i++){
      if(sol[i] != possPath1[i] && sol[i] != possPath2[i]){
          isPath = false;
      }
    }
     passed &= checkCase("Is solution", isPath);
  }
  return passed;
}

bool testMissionariesCannibals(){
  bool passed = true;

  cout << "\nMissionaries Cannibals Solution" << endl;

  MC mc;
  cout << "one";
  Graph<GameState> g = mc.getGraph();
  cout << "two";
  vector<GameState> sol = g.getPath(GameState(3, 3, true), GameState(0, 0, false));
  passed &= checkCase("Solution implemented", sol.size() > 0);
  if(passed){
    for(int i = 0; i < sol.size(); i++){
      cout <<  sol[i] << endl;
    }
    cout << "\nMissionaries Cannibals Test Cases" << endl;
    passed &= checkCase("Solution Size: ", sol.size() == 12);
    passed &= checkCase("Solution Order: ", sol[0] == GameState(3, 3, true));
    passed &= checkCase("Solution Order: ", sol[11] == GameState(0, 0, false));

    bool isValid = true;

    for(int i = 1; i < sol.size(); i++){
      if(!sol[i].isNeighbor(sol[i-1])){
        isValid = false;
      }
    }
    passed &= checkCase("Valid Solution", isValid);
  }
  return passed;
}



Graph<int>populateNumbersGraph(){
  Graph<int>g(10);
  int verts [] = {9, 2, 1, 7, 5, 4, 8, 3, 6, 0};
  for(int i = 0; i < 10; i++){
    g.addVertex(verts[i]);
  }


  g.addEdge(0, 1);
  g.addEdge(0, 2);
  g.addEdge(0, 3);
  g.addEdge(1, 4);
  g.addEdge(4, 5);
  g.addEdge(5, 6);
  g.addEdge(3, 6);
  g.addEdge(6, 7);
  g.addEdge(2, 7);
  g.addEdge(6, 8);
  g.addEdge(5, 8);
  g.addEdge(5, 9);
  g.addEdge(9, 8);

  g.addEdge(1, 0);
  g.addEdge(2, 0);
  g.addEdge(3, 0);
  g.addEdge(4, 1);
  g.addEdge(5, 4);
  g.addEdge(6, 5);
  g.addEdge(6, 3);
  g.addEdge(7, 6);
  g.addEdge(7, 2);
  g.addEdge(8, 6);
  g.addEdge(8, 5);
  g.addEdge(9, 5);
  g.addEdge(8, 9);

  return g;
}


/*
Numbers Undirected Graph

[0]-----------[2]
|  \            \
|   \            \
[1]  [3]----[6]---[7]
|          /  \
|         /    \
|        /      \
|     [5]--------[8]
|    /   \       /
|   /     \     /
|  /       \   /
[4]         [9]

*/

bool checkCase(string name, bool condition){
  if(!condition){
      cout << "Failed: " << name << endl;
  }else{
       cout << "Passed: " << name << endl;
  }return condition;
}