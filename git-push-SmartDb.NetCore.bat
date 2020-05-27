@echo off

cd /d F:\work\dotnetcore-work\gg\SmartDb.NetCore


::提交被修改modified和被删除deleted文件，不包括新文件new
git add -u

::提交新文件new和被修改modified文件，不包括被删除deleted文件
git add .  

::提交文件
git commit -m "Automatic growth value method when adding data"


git remote add origin git@github.com:joyet/SmartDb.NetCore.git


::提交到远程分支
git push  origin master


pause
