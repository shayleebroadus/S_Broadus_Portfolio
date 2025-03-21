//
// Created by shayk on 5/2/2023.
//

#ifndef LEARNDIJKSTRASALGOR_GRAPH_H
#define LEARNDIJKSTRASALGOR_GRAPH_H

#include <list>
#include <string>
#include <fstream>
#include <stdexcept>
#include <cstdint>

using std::ifstream;
using namespace std;
using std::invalid_argument;
using std::list;

struct Pair {
    Pair(int n, int w){
        node = n;
        weight = w;
    }
    int node;
    int weight;
};

class Graph{
public:
    Graph(string filename);
    int shortestCost(int start, int finish);

private:
    list<Pair>* adjList;
    int numNodes;

};


Graph::Graph(string filename){
    ifstream input;
    input.open(filename);
    if(!input.is_open()){
        throw invalid_argument("Could not read filename");
    }

    input >> numNodes;
    adjList = new list<Pair>[numNodes];
    for (int i =0; i < numNodes; i++){
        int value;
        int cost;
        input >> value;
        while(value >=0){
            input >> cost;
            adjList[i].push_back(Pair(value, cost));
            input>> value;

        }
    }
}


int Graph::shortestCost(int start, int finish) {
    int * dist = new int[numNodes];
    list<int> remaining;
    list<int> finished;
    for(int i =0; i < numNodes; i++){
        if(i ==start){
            dist[i] = 0;
        }
        else{
            dist[i]= INT_MAX;
        }
        remaining.push_back(i);
    }//for loop
    while(!remaining.empty()){
        int minDist = INT_MAX;
        int minIndex = -1;
        for (list<int>::iterator listIt = remaining.begin(); listIt != remaining.end(); listIt++){
            if (dist[*listIt] < minDist){
                minDist = dist[*listIt];
                minIndex = *listIt;
            }//while for if

        }// for loop

        int cur = minIndex;
        if(!adjList[cur].empty()){
            for(list<Pair>::iterator listIt = adjList[cur].begin(); listIt != adjList[cur].end(); listIt++){
                int next = (*listIt).node;
                int nextWeight = (*listIt).weight;
                if(dist[cur]+nextWeight < dist[next]){
                    dist[next] = dist[cur] + nextWeight;
                }//if
            }//for
        }
        remaining.remove(cur);
        finished.push_back(cur);
    }//while remain is not empty
    int shortestCost = dist[finish];
    delete[] dist;
    return shortestCost;
}//function



#endif //LEARNDIJKSTRASALGOR_GRAPH_H
