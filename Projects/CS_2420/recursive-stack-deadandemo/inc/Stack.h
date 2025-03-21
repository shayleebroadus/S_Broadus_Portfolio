#ifndef ORDEREDLINKEDLIST_H
#define ORDEREDLINKEDLIST_H
#include <iostream>

template <class Type>
struct Node
{
	Type data;
	Node *next;
};

template <class Type>
class Stack;

template <class Type>
std::ostream& operator<<(std::ostream&, const Stack<Type>& stack);


template <class Type>
class Stack
{
public:
	Stack();
	Stack(const Stack& other);
	Stack<Type>& operator=(const Stack<Type>& other);
	~Stack();

        int size() const;
        bool empty() const;
	Type top() const;
	void push(const Type&);
	void pop();
        void pop(int);
        Type topPop();
        void clear();
        std::string recursive_str(Node<Type> *curr, int count, std::string out ="");
        friend std::ostream& operator<< <>(std::ostream&, const Stack<Type>& list);
    Node<Type>* Top;
    Node<Type>* Tail;
    int sSize;

};

/// stack is Last In First out, Inserts new items at the head or Top

template <class Type>
Stack<Type>::Stack()
{
    Top = nullptr;
    Tail = nullptr;
    sSize= 0;
}

template <class Type>
Stack<Type>::Stack(const Stack<Type>& other)
{
    if (other.sSize ==0){
        this->Top = nullptr;
        this->Tail = nullptr;
        sSize =0;
    }
    else{
        sSize = other.size();
        Top = new Node<Type>;
        auto currNode = Top;
        auto otherNode = other.Top;
        for (int i =0; i < sSize; i++){
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
        Tail = currNode;
    }
}

template <class Type>
Stack<Type>& Stack<Type>::operator=(const Stack& other)
{
    if (other.sSize ==0){
        this->Top = nullptr;
        this->Tail = nullptr;
        sSize =0;
    }
    else{
        sSize = other.size();
        Top = new Node<Type>;
        auto currNode = Top;
        auto otherNode = other.Top;
        for (int i =0; i < sSize; i++){
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
        Tail = currNode;
    }
return *this;
}

template <class Type>
Stack<Type>::~Stack()
{
    auto temp = Top;
    while (temp != Tail){
        auto toDelete = temp;
        temp= temp->next;
        delete toDelete;
    }
    delete Tail;
    Top = nullptr;
    sSize = 0;
}

template <class Type>
int Stack<Type>::size() const
{
return sSize;
}

template <class Type>
bool Stack<Type>::empty() const
{
    if(size() <=0){return true;}
    else{ return false;}
}

template <class Type>
Type Stack<Type>::top() const
{
    if(!empty()){
        auto ret = Top;
        return ret->data;
    }
    else{ throw std::logic_error("");}

}

template <class Type>
void Stack<Type>::push(const Type& item)
{
    auto newNode = new Node<Type>;
    newNode->data = item;
    auto currNode = Top;
    newNode->next = currNode;
    Top = newNode;
    sSize++;
}

template <class Type>
void Stack<Type>::pop()
{
    if(!empty()){
        auto currNode = Top;
        Top = currNode->next;
        delete currNode;
        currNode = nullptr;
        sSize--;
    }
}

template <class Type>
void Stack<Type>::pop(int count)
{
    if(count < sSize){
        while(count !=0){
            this->pop();
            count--;
        }
    }
    else if(count >= sSize){
        this->clear();
    }
}

template <class Type>
Type Stack<Type>::topPop()
{
    // pop top and return its data value.
    if(sSize > 0){
        auto value = Top->data;
        auto toDelete = Top;
        Top = Top->next;
        delete toDelete;
        sSize--;
        return value;

    }
    if(sSize==0){
        throw std::logic_error("");
    }

}

template <class Type>
void Stack<Type>::clear()
{
    auto temp = Top;
    while (temp != Tail){
        auto toDelete = temp;
        temp= temp->next;
        delete toDelete;
    }
    delete Tail;
    Top = nullptr;
    Tail = nullptr;
    sSize = 0;
}

template <class Type>
std::string recursive_str(Node<Type> *curr, int count, std::string out= ""){
    //curr is originally equal to Top
   if(count ==1){
       out+= std::to_string(curr->data);
       return out;
   }
   else if(count ==0){
       return out;
   }

   out+= recursive_str(curr->next,count-1, out);
   out+= "->"+std::to_string(curr->data);

    return out;

}

template <class Type>
std::ostream& operator <<(std::ostream &out, Stack<Type> &list){
    auto currNode= list.Top;
    std::string output = recursive_str(currNode, list.size());
    out << output;

    return out;
}
#endif
