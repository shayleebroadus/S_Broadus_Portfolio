cmake_minimum_required(VERSION 3.24)
project(Queue)

set(CMAKE_CXX_STANDARD 17)

add_executable( Queue QueueTesting.cpp Queue.h QueueTesting.cpp)
