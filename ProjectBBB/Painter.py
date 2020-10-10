# -*- coding: utf-8 -*-
"""
Created on Sun Sep 20 10:56:10 2020

@author: 29450
"""

from py2neo import Graph, Database 
from py2neo.data import Node, Relationship, Subgraph, walk
from py2neo.matching import *
from py2neo.ogm import *
import numpy as np
import re

from BearMachine import BearMachine as BearMachine 

class Painter():
    def __init__(self,graph):
        self.graph = graph
        pass
    
    def paint(self,tag):
        pass
    
    
    pass

class Canvas():
    def __init__(self,graph):
        self.graph = graph
        self.color_dic = {}

        pass
    
    def get_nodes(self,strings):
        rs = []
        dic = self.color_dic
        for key in strings:
            if type(key) is int:
                n = get_node()
                rs.append(n)
                dic[n] = key
            elif type(key) is str:
                n = get_node(color = key)
                rs.append(n)
                
        return rs
    pass

    def connect(self):
        dic2 ={}
        pool = []
        dic = self.color_dic
        for node in dic.keys():
            key = dic[node]
            if key in dic2.keys():
                last = dic2[key]
                rel = Relationship(last,"is",node)
                pool.append(rel)
                
            dic2[key] = node
            
            pass
        return pool

def get_node(tag = None, color = None):
    n = Node()
    if color is not None:
        n["color"] = color 
    
    if tag is not None:
        n.add_label(tag)
        
    return n

def test(graph):
    bm = BearMachine(graph)
    graph.delete_all()

    
    bm.hear_strings("0123456789")
    bm.cut()
    bm.hear_strings("1+x=y")
    pass

def append_next(graph,end,next_node):
    graph.create(next_node)
   
    rel = Relationship(end,"t",next_node, r = 0, d = 1)
    
    graph.create(rel)
    pass

def append_is(graph,end,next_node):
    graph.create(next_node)
   
    rel = Relationship(end,"is",next_node)
    
    graph.create(rel)
    pass

if __name__ == "__main__":
    graph =  Graph(password="Bb19980123")
   # test(graph)    
    
    c = Canvas(graph)
    nodes1 = c.get_nodes(["1","+",0,"=",1])
    nodes2 = c.get_nodes([0,1])
    
   
    
    print(c.connect())
    
   