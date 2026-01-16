from django.db import models
from datetime import datetime
import pytz # $ pip install pytz
from django.conf import settings  
from pytz import timezone
import os
# Create your models here.
#from django.contrib.postgres.fields import HStoreField

def get_path(instance, filename):
    return '{0}/{1}'.format(instance.sid, filename)


class mv(models.Model):
    장르 = models.CharField(max_length=10)
    제목 = models.CharField(max_length=500)
    
    def __str__(self):
        return self.장르

