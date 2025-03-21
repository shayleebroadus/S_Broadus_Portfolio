#ifndef GRAPH_H
#define GRAPH_H
#include <stack>
#include <list>
#include <stack>
#include <vector>
#include <iostream>

using std::endl;
using std::cout;
using std::ostream;
using std::stack;
using std::vector;
using std::list;


template<typename T>
struct Node {
    Node(T d){
        data = d;
       // edges.push_back(e);
    }
    T data;
    vector<T> edges;
};

template <typename T>
class Graph;

template <typename T>
ostream& operator << (ostream & out, Graph<T> g);

template <typename T>
class Graph{
    private:
      //Declare any variables needed for your graph
      int numVertices =0;
      vector<Node<T>> Vert;
      int capacity;
    public:
      Graph();
      Graph(int);
      void addVertex(T vertex);
      void addEdge(T source, T target);
      vector<T> getPath(T, T);
      int findVertexPos(T item);
      int getNumVertices();
      friend ostream& operator << <>(ostream & out, Graph<T> g);
};



/*********************************************
* Constructs an empty graph with a max number of verticies of 100
* 
*********************************************/
template<typename T>
Graph<T>::Graph(){
    capacity = 100;
}


/*********************************************
* Constructs an empty graph with a given max number of verticies
* 
*********************************************/
template<typename T>
Graph<T>::Graph(int maxVertices){
    capacity = maxVertices;
}




/*********************************************
* Adds a Vertex to the GraphIf number of verticies is less than the 
* Max Possible number of verticies.  
*********************************************/
template <typename T>
void Graph<T>::addVertex(T vertex){
    if(numVertices< capacity){
        auto curr = Node<T>(vertex);
        //get the next "open" element in the list
        Vert.push_back(curr);
        numVertices++;
    }
}

/*********************************************
* Returns the current number of vertices
* 
*********************************************/
template<typename T>
int Graph<T>::getNumVertices(){
  return numVertices;
}



/*********************************************
* Returns the position in the verticies list where the given vertex is located, -1 if not found.
* 
*********************************************/
template <typename T>
int Graph<T>::findVertexPos(T item){
   for(int i =0; i < Vert.size(); i++){
       if(Vert[i].data == item){
           return i;
       }
   }
    return -1; //return negative one
}//End findVertexPos

/*********************************************
* Adds an edge going in the direction of source going to target
* 
*********************************************/
template <typename T>
void Graph<T>::addEdge(T source, T target){
   int ndx = findVertexPos(source);
   auto curr = Node<T>(source);
   Vert[ndx].edges.push_back(target);
}


/*********************************************
* Returns a display of the graph in the format
* vertex: edge edge
Your display will look something like the following
    9: 8 5
    2: 7 0
    1: 4 0
    7: 6 2
    5: 6 8 9 4
    4: 5 1
    8: 6 5 9
    3: 6 0
    6: 7 8 5 3
    0: 1 2 3
*********************************************/
template <typename T>
ostream& operator << (ostream & out, Graph<T> g){
    for (int i =0; i < g.Vert.size(); i++){
        //prints out the name or number of the node NOT THE INDEX
        out<< g.Vert[i].data << ": ";
        for(int j =0; j < g.Vert[i].edges.size(); j++){
            out<< g.Vert[i].edges[j] <<" ";
        }
        out << "\n";
    }
    return out;
}




/*
  getPath will return the shortest path from source to dest.  
  You may use any traversal/search algorithm you want including
  breadth first, depth first, dijkstra's algorithm, or any
  other graph algorithm.
  You will return a vector with the solution.  The source will be in position 1
  the destination is in the last position of the solution, and each node in between 
  are the verticies it will travel to get to the destination.  There will not be any
  other verticies in the list.

  Given the test graph:
  
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

getPath(0, 5) should return 
0 -> 1 -> 4 -> 5   or   0 -> 3 -> 6 -> 5
    
  Hint: As you find the solution store it in a stack, pop all the items of the stack 
  into a vector so, it will be in the correct order.

*/
template <typename T>
vector<T> Graph<T>::getPath(T source, T dest){
  vector<T> solution;
  vector<T> From;
  vector<Node<T>> stack;
  bool foundDest = false;


  int ndx = findVertexPos(source);
  From.push_back(source);
  stack.push_back(Vert[ndx]);


  for(int i =0; i < Vert.size(); i++){
      T curr = stack[i].data;
      for(int j = 0; j < stack[i].edges.size(); j++){
          T value = stack[i].edges[j];
          bool visitCheck = false;
          for(int k = 0; k < stack.size(); k++){
              if(value == stack[k].data){
                  visitCheck = true;
                  break;
              }
          }//inner inner loop
          if(!visitCheck){
              From.push_back(curr);
              int index = findVertexPos(stack[i].edges[j]);
              stack.push_back(Vert[index]);
          }
          if(value == dest){
              foundDest = true;
              break;
          }
      }//inner for loop
      if(foundDest){
          break;
      }
  }//outer for loop

  //stack end pos (7) -> from[7] ->  from[ value 4] -> from[ value 1]
  //               5  ->   T 4    ->        1        ->       0

  //create a stack to "flip" around the order of the vertices traveled.
  std::stack<T>cords;
  //get the last element in stack

  int nextNdx=stack.size()-1;
  cords.push(stack[nextNdx].data);
  T temp = From[nextNdx];

  while(true){
      cords.push(temp);
      for(int i =0; i < stack.size(); i++){
          if(stack[i].data == temp){
              temp = From[i];
              break;
          }
      }
      if(temp==source){
          break;
      }
  }
    cords.push(source);

  //add the values in cords to the Solution vector
  while(!cords.empty()){
      solution.push_back(cords.top());
      cords.pop();
  }

  return solution;
}

#endif
