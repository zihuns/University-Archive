# -*- coding: utf-8 -*-
from django.shortcuts import render, redirect
from .forms import mvForm, mvForm2
from .models import mv
from django.core.files.storage import FileSystemStorage
from django.views.generic import TemplateView, ListView, CreateView
from django.urls import reverse_lazy
from datetime import datetime
from django.contrib import messages
from django.http import *
import pytz # $ pip install pytz
import pickledb
#utc_offset = datetime.now(pytz.timezone('Asia/Seoul')).utcoffset()
#from key_value_db import KeyValueDB, KeyValueObject

db = pickledb.load('example.db', False)

def upload_file(request):
    form = mvForm2()
    files = mvForm(None)
    if request.method == 'POST':
        
        key = request.POST.get('장르')
        files = mv.objects.filter(장르=db.get(key))

    elif request.method == 'GET':
        form = mvForm2(request.GET)

        
    context = {'files': files, 'form':form,}
    return render(request, 'mv/upload_file.html', context)













def kvstore(request):
    
    #initialize the key-value
    key_value_db = KeyValueDB("Greet=Hello World,Project=KeyValueDB ", True, '=', ',', False)

    #get an object
    print(key_value_db.get("Greet"))
    print(key_value_db.get("Project"))

    #remove an object
    key_value_db.remove("Greet")

    #add an object
    key_value_db.add("What", "i don't know what to write here")
    key_value_db.__str__()
    #print all the objects
    for kvo in key_value_db:
        print(kvo)
    kv = key_value_db.get("What")
    context = {'kv': kv,}
    return render(request, 'mv/upload_file.html', context)





