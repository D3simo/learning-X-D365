# Forupdate

Select firstonly forupdate custTable
    where custTable.AccountNum == '1001';

    if(custTable)
    {
        ttsbegin;
        custTable.custGroup == '80';
        custTable.update();       
        ttscommit;
    }


==


Select firstonly custTable
    where custTable.Accountnum = '1001';

    if(custTable)
    {
        ttsbegin;
        custTable.selectForUpdate(true);
        custTable.custGroup == '80';
        custTable.update();       
        ttscommit;
    }