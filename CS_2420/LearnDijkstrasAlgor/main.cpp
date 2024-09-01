#include <iostream>
#include "Graph.h"
using namespace std;
int main() {

    Graph g1("C:\\Users\\shayk\\OneDrive\\Desktop\\CS 2420\\Module 7\\LearnDijkstrasAlgor\\graph.txt");
    cout << "0->3: " << g1.shortestCost(0,3) << endl;
    cout << "0->1: " << g1.shortestCost(0, 1) << endl;
    cout << "0->2: " << g1.shortestCost(0, 2) << endl;
    return 0;
}
