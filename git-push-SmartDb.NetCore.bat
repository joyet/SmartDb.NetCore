@echo off

cd /d F:\work\dotnetcore-work\gg\SmartDb.NetCore


::提交被修改(modified)和被删除(deleted)文件，不包括新文件(new)
git add -u

::提交新文件(new)和被修改(modified)文件，不包括被删除(deleted)文件
git add .  

::提交文件
git commit -m "Upate Push Bat"


::git remote add origin git@github.com:joyet/SmartDb.NetCore.NuGet.git

git push  origin master


pause
