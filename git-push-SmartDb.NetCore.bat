@echo off

cd /d F:\work\dotnetcore-work\gg\SmartDb.NetCore


::�ύ���޸�modified�ͱ�ɾ��deleted�ļ������������ļ�new
git add -u

::�ύ���ļ�new�ͱ��޸�modified�ļ�����������ɾ��deleted�ļ�
git add .  

::�ύ�ļ�
git commit -m "Automatic growth value method when adding data"


git remote add origin git@github.com:joyet/SmartDb.NetCore.git


::�ύ��Զ�̷�֧
git push  origin master


pause
