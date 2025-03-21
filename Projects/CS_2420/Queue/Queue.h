//
// Created by shayk on 3/19/2023.
//


#ifndef QUEUE_QUEUE_H
#define QUEUE_QUEUE_H
#include <iostream>

template <class Type>
struct Node
{
    Type data;
    Node *next;
};

template <class Type>
class Queue;

template <class Type>
std::ostream& operator<<(std::ostream&, Queue<Type>& queue);


template <class Type>
class Queue
{
public:
    Queue();                            //sets the front and back pointer to nullptr and initializes any other additional variables
    Queue(const Queue<Type>& other);    //Copy Constructor, creates a deep copy of the other stack
    ~Queue();                           //Deallocates memory by deleting all the existing nodes in the Queue

    void enqueue(Type item);            //Adds an item to the end of the queue
    void dequeue();                     //removes an item from the front of the queue
    Type peek();                        //returns the value of the item at the front of the queue, but does not remove it
    int size();                         //Returns the number of items in the queue
    bool empty();                       //returns true if the queue is empty
    void clear();                       //deletes the items in the list and sets the size to 0 (don't forget to delete each node using the "delete" command.)
    friend std::ostream& operator << <>(std::ostream &out, Queue<Type> &q);             //explanation below
    std::string recursive_str(Node<Type> *curr);


protected:
    Node<Type>* head;
    Node<Type>* tail;
    int qSize;
};
/*
 friend ostream &operator<< <>(ostream &out, Queue<Type> &q); Allows the user to output the queue
 formatted as... item1->item2->item3->item4.  For example, if the queue has values 1, 2, 3, 4 with 1 at the front and 4
 at the end it would return 1->2->3->4  It is recommended that you use recursion to accomplish this function.  You may
 use a private recursive to code the logic, then the operator would just return the results of the recursive function
 such as...  string recursive_str(Node<Type> *curr);
 */
template <class Type>
    Queue<Type>::Queue(){
        head = nullptr;
        tail = nullptr;
        qSize = 0;
    }

template <class Type>
    Queue<Type>::Queue(const Queue<Type>& other){
    if(other.qSize == 0){
        qSize = 0;
        this->head = nullptr;
        this->tail = nullptr;
    }
    else{
        qSize = other.qSize;
        head = new Node<Type>;
        auto currNode = head;
        auto otherNode = other.head;
        for (int i =0; i < qSize; i++){
            currNode->data = otherNode->data;
            //allocate memory for the next node
            auto nextNode = new Node<Type>;
            nextNode->next = nullptr;
            if (otherNode->next != nullptr){
                currNode->next = nextNode;
                currNode = currNode->next;
            }
            otherNode= otherNode->next;
        }
        tail = currNode;
    }

    }

template <class Type>
    Queue<Type>::~Queue(){
    Node<Type>* temp = head;
    while(qSize !=0){
        temp = head;
        head = head->next;
        delete temp;
        qSize--;
    }
    head = nullptr;
    tail = nullptr;
    qSize = 0;
    }


template <class Type>
    void Queue<Type>::enqueue(Type item){
    // items are always appending to the back of a queue, like standing in a line
    auto newNode = new Node<Type>;
    newNode->data = item;
    if (qSize ==0){
        newNode->next = nullptr;
        head = newNode;
        tail = newNode;
        qSize = 1;
    }
    else if (qSize >0){
        //get the node at the end
        auto currNode = tail;
        currNode->next = newNode;
        tail = newNode;
        newNode->next = nullptr;
        qSize++;
    }

    }

template <class Type>
    void Queue<Type>::dequeue(){
        //get the first node and set head to the next;
        auto currNode = head;
        if(qSize > 1){
            head = head->next;
            //delete the old node
            delete currNode;
            qSize--;
        }
        else if(qSize ==1){
            head = nullptr;
            tail = nullptr;
            qSize = 0;
        }
    }

template <class Type>
    Type Queue<Type>::peek(){
        //returns data at head node
        if (qSize >=1){
            return head->data;
        }
        else {
            throw std::out_of_range("");
        }
    }

template <class Type>
    int Queue<Type>::size(){

        return qSize;
    }

template <class Type>
    bool Queue<Type>::empty(){
        if (qSize == 0){
            return 1;
        }
        return 0;
    }

template <class Type>
    void Queue<Type>::clear(){
    auto currNode = head;
    while (currNode->next){
        auto toDelete = currNode;
        currNode = currNode->next;
        delete toDelete;
        qSize--;
    }
    head = nullptr;
    tail= nullptr;
    qSize = 0;
    }

    /// *****************coding credit to Ryan O'Connor (from the discussion board)************
template <class Type>
std::string recursive_str(Node<Type> *curr){
    std::string out = "";
    if(curr->next){
        out = std::to_string(curr->data) + "->" + recursive_str(curr->next);
    }
    else{
        out = std::to_string(curr->data);
    }
    return out;
}
///****************************************************************************

template <class Type>
    std::ostream& operator <<(std::ostream &out, Queue<Type> &q){
       auto currNode= q.head;
       std::string output = recursive_str(currNode);
       out << output;

        return out;
    }



#endif //QUEUE_QUEUE_H
