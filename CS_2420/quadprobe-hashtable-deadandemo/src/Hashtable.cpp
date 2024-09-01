#include "Hashtable.h"

/// quadprobe equation = hash() + 0*i+2*i^2
///load factor is calculated as: sz/cap

int Hashtable::hash(int v) const{
   return v % capacity();
}
 
Hashtable::Hashtable()
{
    sz = 0;
    cap = 17;
    table = new int[cap];
    for(int i =0; i < cap; i++){
        table[i] = NULL;
    }
    factor = 0.65;
}

Hashtable::Hashtable(int capacity)
{
    sz = 0;
    cap = capacity;
    table = new int[cap];
    for(int i =0; i < cap; i++){
        table[i] = NULL;
    }
    factor = 0.65;
}

Hashtable::Hashtable(int capacity, double threshold)
{
    sz = 0;
    cap = capacity;
    table = new int[cap];
    for(int i =0; i < cap; i++){
        table[i] = NULL;
    }
    factor = threshold;
}

Hashtable::Hashtable(const Hashtable& other)
{
    sz = other.sz;
    cap = other.cap;
    factor = other.factor;
    table = new int[cap];
    for(int i =0; i < cap; i++){
        table[i] = other.table[i];
    }
}

Hashtable& Hashtable::operator=(const Hashtable& other)
{
    delete[] table;
    sz = other.sz;
    cap = other.cap;
    factor = other.factor;
    table = new int[cap];
    for(int i =0; i < cap; i++){
        table[i] = other.table[i];
    }

return *this;
}

Hashtable::~Hashtable()
{
    delete[]table;
}

int Hashtable::size() const
{
return sz;
}

int Hashtable::capacity() const
{
return cap;
}
double Hashtable::getLoadFactorThreshold() const
{
   return factor;
}

bool Hashtable::empty() const
{
return sz==0;
}

void Hashtable::insert(int value)
{
    //check if full

    //factor =  0.65
    //double check = (sz)/cap;
    sz++;
    if(sz/(double) cap>=factor){
        int newCap = cap*2;
        int prime =nextPrime(newCap);
        int temp[prime];
        int tempCap = cap;
        cap = prime;
        for(int i= 0; i < tempCap; i++){
            temp[i] = table[i];
        }
        //delete table and recreate it with temp
        delete [] table;
        table = new int[cap];
        for(int i =0; i < cap; i++){
            table[i] = NULL;
        }
        sz = 0;
        for(int i =0; i < tempCap; i++){
            if(temp[i]!=0){
                insert(temp[i]);
            }
        }
    }

    int i =0;
    int bucketsProbed = 0;
    int bucket = hash(value);
    while(bucketsProbed < cap){

        if(table[bucket]==NULL){
            table[bucket] = value;
            break;
        }
        i++;
        bucket = hash(hash(value) + (0 *i) + (1*i*i));

        bucketsProbed++;
    }//while loop

}//insert function

void Hashtable::remove(int value)
{
    if(contains(value)){
        int ndx = indexOf(value);
        table[ndx] = NULL;
        sz--;
    }


}

bool Hashtable::contains(int value) const{
    int i =0;
    int bucketsProbed = 0;
    int bucket = hash(value);
    while(bucketsProbed < cap){
        if(table[bucket]==value){
            return true;
        }
        i++;
        bucket = hash(hash(value) + (0 *i) + (1*i*i));

        bucketsProbed++;
    }//while loop
return false;
}

int Hashtable::indexOf(int value) const
{
//    for(int i =0; i < cap; i++){
//        if(table[i]){
//            std::cout<< i << ": " <<table[i] <<" -> ";
//        }
//        else
//        {
//            std::cout<< "Null ->";
//        }
//    }
//    std::cout<< std::endl;
    int i =0;
    int bucketsProbed = 0;
    int bucket = hash(value);

        while(bucketsProbed < cap){
            if(table[bucket]==value){
                return (bucket);
            }
            i++;
            bucket = hash(hash(value) + (0 *i) + (1*i*i));

            bucketsProbed++;
        }//while loop

   return -1;
}

void Hashtable::clear()
{
    for (int i =0; i < cap; i++){
        if(table[i]){
            table[i]=NULL;
        }
    }
    sz =0;
}

bool Hashtable::isPrime(int n){
    if (n <= 1){
        return false;
    }
    for(int i = 2; i < n; i++){
        if(n%i==0){
            return false;
        }
    }

    return true;
}

int Hashtable::nextPrime(int n){
   while(!isPrime(n)){
       n++;
   }
   return n;
}
