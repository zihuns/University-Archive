# -*- coding: utf-8 -*-
from django import forms
from .models import mv

#'webkitdirectory': True, 'directory': True}
class mvForm(forms.ModelForm):
        CHOICES = (
                    (11, '드라마'),
                    (12, '로맨스'),
                    (13, '범죄'),
                    (21, '스릴러'),
                    (22, '액션'),
                    (31, '호러'),
                    (32, 'Restaurants'),
                  )
        장르 = forms.ChoiceField(choices=CHOICES)
        
        class Meta:
           model = mv
           fields = ['장르',]

class mvForm2(forms.ModelForm):
        제목 =  forms.CharField(max_length=100)
        
        class Meta:
           model = mv
           fields = ['제목',]