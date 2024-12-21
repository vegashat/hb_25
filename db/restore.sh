echo "Starting sqlserver"
/opt/mssql/bin/sqlservr  > start_up.txt 2>1 &

echo "Waiting for sql server to start"
work_done=0
time_cnt=0
while [ $work_done -eq 0 ]
do
sleep 3
grep "Recovery is complete" start_up.txt
if [ $? -eq 0 ] 
then
   echo "work done becausae database back up"

    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'MilkHoney1' -C -Q "sp_configure 'CONTAINED DATABASE AUTHENTICATION', 1;RECONFIGURE;"
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'MilkHoney1' -C -Q "RESTORE Database [homeboard] FROM DISK = N'/tmp/hb.bak' with replace"

   pd=$(pgrep sqlservr)
   echo $pd
   kill -9 $pd
   work_done=1
else
   time_cnt=$((time_cnt + 1))
   if test $time_cnt -eq 10 
   then
      echo "work done because timeout threshold exceeded"
      work_done=1
   fi
fi
done