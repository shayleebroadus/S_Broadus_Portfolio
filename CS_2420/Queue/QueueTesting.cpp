#include <iostream>
#include <string>
#include <cstdio>
#include <sstream>
#include "Queue.h"

using namespace std;
//Code to check the queue
void testConstructor();
void testCopyConstructor();
void testQueue();
void testEqualOperator();
//Checks to see if the results match
bool checkTest(string, int, int);
bool checkTest(string, string, string);

int main()
{
    cout << "Testing queue\n" << endl;
    testConstructor();
    testQueue();
    testCopyConstructor();
    testEqualOperator();

    return 0;
}

void testConstructor(){
   Queue<int> iQueue;
   checkTest("Constructor 1", iQueue.size(), 0);
   checkTest("Constructor 2", iQueue.empty(), true);
   Queue<double> dQueue;
   checkTest("Constructor 3", dQueue.size(), 0);
   checkTest("Constructor 4", dQueue.empty(), true);
}

//This helps with testing, do not modify.
void testQueue() {

  // Last in, first out data structure (LIFO)

  string result;
  string caughtError;
  {
    Queue <int> queue;

    queue.enqueue(1);
    queue.enqueue(2);
    queue.enqueue(3);
    queue.enqueue(4);
    queue.enqueue(5);
    checkTest("Queue 1 Adding", 1, queue.peek());
    queue.dequeue();
    checkTest("Queue 2 Adding", 2, queue.peek());
    queue.dequeue();
    queue.dequeue();
    checkTest("Queue 3 Adding", 4, queue.peek());
    queue.dequeue();
    queue.dequeue();
    // Now cover error handling

    try {
      result = queue.peek();
    }
    catch (exception &e) {
      caughtError = "caught";
    }
    checkTest("Queue #6: Viewing empty list exception ", "caught", caughtError);

    // Test some strings
    Queue<string> squeue;

    squeue.enqueue("pencil");
    squeue.enqueue("pen");
    squeue.enqueue("marker");


    // Remove pen from the queue.
    string temp = squeue.peek(); // Get Pencil
    squeue.dequeue();               // Remove Pencil
    squeue.dequeue();               // Remove pen
    squeue.enqueue(temp);          // Push Pencil back in

                                 // See if it worked
    checkTest("Queue #7 maintaining queue order", "marker", squeue.peek());
    squeue.dequeue();

   Queue<int> q;
   q.enqueue(1);
   q.enqueue(2);
   q.enqueue(3);
   q.enqueue(4);
   q.enqueue(5);
   string str = "1->2->3->4->5";
   ostringstream output;
   output << q;

   checkTest("Queue #8 printing queue", str, output.str());
   q.clear();


    checkTest("Queue #9 clear method size", 0, q.size());
    checkTest("Queue #10 clear method empty", true, q.empty());
    output.str("");
    output.clear();
    q.enqueue(7);
    output << q;
    checkTest("Queue #9 clear method then add", "7", output.str());

  }

}


void testCopyConstructor(){

    Queue<int> iQueue;
    for(int i = 0; i < 10; i++){
        iQueue.enqueue(i);
    }
    Queue<int>iQueue2(iQueue);
    ostringstream output;
    output << iQueue2;
    checkTest("Copy Constructor 1", iQueue.size(), 10);
    checkTest("Copy Constructor 2", "0->1->2->3->4->5->6->7->8->9", output.str());
    iQueue.dequeue();
    iQueue.dequeue();
    output.str("");
    output.clear();
    output << iQueue2;
    checkTest("Copy Constructor 3", "0->1->2->3->4->5->6->7->8->9", output.str());

}

void testEqualOperator(){
    Queue<int>one;
    for(int i = 0; i < 10; i++){
        one.enqueue(i);
    }

    Queue<int>two = one;
    ostringstream output;
    output << two;
    checkTest("Equal Operator 1", "0->1->2->3->4->5->6->7->8->9", output.str());
    one.dequeue();
    output.str("");
    output.clear();
    output << two;
    checkTest("Equal Operator 1", "0->1->2->3->4->5->6->7->8->9", output.str());
}
//This helps with testing, do not modify.
bool checkTest(string testName, int whatItShouldBe, int whatItIs) {

  if (whatItShouldBe == whatItIs) {
    cout << "Passed " << testName << endl;
    return true;
  }
  else {
    cout << "***Failed test " << testName << " *** " << endl << "   Output was " << whatItIs << endl << "   Output should have been " << whatItShouldBe << endl;
    return false;
  }
}


//This helps with testing, comment it in when ready, but do not modify the code.
bool checkTest(string testName, string whatItShouldBe, string whatItIs) {

  if (whatItShouldBe == whatItIs) {
    cout << "Passed " << testName << endl;
    return true;
  }
  else {
    if (whatItShouldBe == "") {
      cout << "***Failed test " << testName << " *** " << endl << "   Output was " << whatItIs << endl << "   Output should have been blank. " << endl;
    }
    else {
      cout << "***Failed test " << testName << " *** " << endl << "   Output was " << whatItIs << endl << "   Output should have been " << whatItShouldBe << endl;
    }
    return false;
  }
}




