#include "ParenthesesMatch.h"
#include "Stack.h"

bool parenthesesMatch(const char* str){
    //check the string for "(" and add to stack when found
    int i = 0;
    Stack<char> par;
    while (str[i] != '\0'){
        //push open parenthesis
        if (str[i]== '('){
            par.push(str[i]);
        }
        //pop if close parenthesis and size is greater than 0
        else if(str[i]== ')' && par.size() > 0) {
            par.pop();
        }
        i++;
    }
    // if sSize>0, parentheses do not match
    if(par.size() > 0){
        return false;
    }
    //if sSize==0, parentheses match return true
    else if(par.size() ==0){
        return true;
    }
    return 0;
}
