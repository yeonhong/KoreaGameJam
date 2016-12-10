#pragma strict
var A : float;//A회사
var B : float;//B회사
var C : float;//C회사
var D : float;//총합
var abc : GameObject;
var a = 0;
var arr : int[] = new int[104];//가능값을 넣은 배열
var srr : int[] = new int[9];
var str : String[] = new String[104];
var sstr : String[] = new String[9];
function Start () {
   
}



function SG (){
        
    A=PlayerPrefs.GetInt("Change0");
    B = PlayerPrefs.GetInt("Change1");;
    C = -PlayerPrefs.GetInt("Change2");;
    D =A+B+C;
    a=1;
}










 





function Update () {
    if(a==1){
        
        for(var i = 1; i<104;i++){
            print(i);
            arr[i]=0;
        }



        for(var k = 1; k<9;k++){
            srr[k]=0;
        }
        str[1] = "전체적으로 주식시장이 상향세이다.";
        str[2] = "전체적으로 주식시장이 하향세이다.";
        str[3]= "전체적으로 주식시장이 동결이다.";

        str[4] ="A회사는 상향세이다.";
        str[5]= "A회사는 상향세가 아니다.";
        str[6]= "A회사는 하향세이다.";
        str[7] = " A회사는 하향세가 아니다.";
        str[8] = "A회사는 동결이다.";
        str[9] = "A회사는 동결이 아니다.";

        str[10] =  "B회사는 상향세이다.";
        str[11] = " B회사는 상향세가 아니다.";
        str[12] = " B회사는 하향세이다.";
        str[13] = " B회사는 하향세가 아니다.";
        str[14] = " B회사는 동결이다.";
        str[15] = " B회사는 동결이 아니다.";

        str[16] = " C회사는 상향세이다.";
        str[17] = " C회사는 상향세가 아니다.";
        str[18] = " C회사는 하향세이다.";
        str[19] = " C회사는 하향세가 아니다.";
        str[20] = " C회사는 동결이다.";
        str[21] = " C회사는 동결이 아니다.";

        str[22] = " A회사는 1등이다.";
        str[23] = " A회사는 2등이다.";
        str[24] = " A회사는 3등이다.";
        str[25] = " A회사는 1등이 아니다.";
        str[26] = " A회사는 2등이 아니다.";
        str[27] = " A회사는 3등이 아니다.";

        str[28] = " B회사는 1등이다.";
        str[29] = " B회사는 2등이다.";
        str[30] = " B회사는 3등이다.";
        str[31] = " B회사는 1등이 아니다.";
        str[32] = " B회사는 2등이 아니다.";
        str[33] = " B회사는 3등이 아니다.";

        str[34] = " C회사는 1등이다.";
        str[35] = " C회사는 2등이다.";
        str[36] = " C회사는 3등이다.";
        str[37] = " C회사는 1등이 아니다.";
        str[38] = " C회사는 2등이 아니다.";
        str[39] = " C회사는 3등이 아니다.";

        str[40] = " 동결하는 주식이 없다.";
        str[41] = " 상향하는 주식이 없다.";
        str[42] = " 하향하는 주식이 없다.";

        str[43] = " 상향하는 주식의 수가 1개이다.";
        str[44] = " 상향하는 주식의 수가 2개이다.";

        str[45] = " 하향하는 주식의 수가 1개이다.";
        str[46] = " 하향하는 주식의 수가 2개이다.";

        str[47] = " A회사는 B회사보다 상향이다.";
        str[48] = " B회사는 A회사보다 하향이다.";

        str[49] = " A회사는 C회사보다 상향이다.";
        str[50] = " C회사는 A회사보다 하향이다.";

        str[51] = " B회사는 C회사보다 상향이다.";
        str[52] = " C회사는 B회사보다 하향이다.";

        str[53] = " A회사는 B회사보다 하향이다.";
        str[54] = " B회사는 A회사보다 상향이다.";

        str[55] = " A회사는 C회사보다 하향이다.";
        str[56] = " C회사는 A회사보다 상향이다.";

        str[57] = " B회사는 C회사보다 하향이다.";
        str[58] = " C회사는 B회사보다 상향이다.";

        str[59] = " 전체주식 시장의 합은 "+D.ToString()+"%이다.";
        if(A>B){
            if(A>C){
                if(B>C){//123
                    str[60] = " 1등주식과 3등주식의 폭은 "+(A-C).ToString()+"%이다.";
                    str[71] = " 3등주식은 "+C.ToString()+"% 상향이다.";
                    str[72] = " 3등주식은 "+(-C).ToString()+"% 하향이다.";
        
                    str[61] = " 2등주식과 3등주식의 폭은 "+(B-C).ToString()+"%이다.";
                    str[67] = " 2등주식은 "+B.ToString()+"% 상향이다.";
                    str[68] = " 2등주식은 "+(-B).ToString()+"% 하향이다.";
                    str[62] = " 1등주식과 2등주식의 폭은 "+(A-B).ToString()+"%이다.";
                    str[63] = " 1등주식은 "+A.ToString()+"% 상향이다.";
                    str[64] = " 1등주식은 "+(-A).ToString()+"% 하향이다.";
                    if(A>0){
                        str[66] = " 1등주식은 "+A.ToString()+"%의 변동을 보인다.";
                    }else{
                        str[66] = " 1등주식은 "+(-A).ToString()+"%의 변동을 보인다.";
                    }
                    if(B>0){
                        str[70] = " 2등주식은 "+B.ToString()+"%의 변동을 보인다.";
                    }else{
                        str[70] = " 2등주식은 "+(-B).ToString()+"%의 변동을 보인다.";
                    }
                    if(C>0){
                        str[74] = " 3등주식은 "+C.ToString()+"%의 변동을 보인다.";
                    }else{
                        str[74] = " 3등주식은 "+(-C).ToString()+"%의 변동을 보인다.";
                    }

                }else{//132
                    str[71] = " 3등주식은 "+B.ToString()+"% 상향이다.";
                    str[72] = " 3등주식은 "+(-B).ToString()+"% 하향이다.";
        
                    str[60] = " 1등주식과 3등주식의 폭은 "+(A-B).ToString()+"%이다.";
                    str[63] = " 1등주식은 "+A.ToString()+"% 상향이다.";
                    str[64] = " 1등주식은 "+(-A).ToString()+"% 하향이다.";
                    str[61] = " 2등주식과 3등주식의 폭은 "+(C-B).ToString()+"%이다.";
                    str[67] = " 2등주식은 "+C.ToString()+"% 상향이다.";
                    str[68] = " 2등주식은 "+(-C).ToString()+"% 하향이다.";
                    str[62] = " 1등주식과 2등주식의 폭은 "+(A-C).ToString()+"%이다.";
                    if(A>0){
                        str[66] = " 1등주식은 "+A.ToString()+"%의 변동을 보인다.";
                    }else{
                        str[66] = " 1등주식은 "+(-A).ToString()+"%의 변동을 보인다.";
                    }
                    if(C>0){
                        str[70] = " 2등주식은 "+C.ToString()+"%의 변동을 보인다.";
                    }else{
                        str[70] = " 2등주식은 "+(-C).ToString()+"%의 변동을 보인다.";
                    }
                    if(B>0){
                        str[74] = " 3등주식은 "+B.ToString()+"%의 변동을 보인다.";
                    }else{
                        str[74] = " 3등주식은 "+(-B).ToString()+"%의 변동을 보인다.";
                    }

                }
            }else{//231
                str[71] = " 3등주식은 "+A.ToString()+"% 상향이다.";
                str[72] = " 3등주식은 "+(-A).ToString()+"% 하향이다.";
        
                str[60] = " 1등주식과 3등주식의 폭은 "+(B-A).ToString()+"%이다.";
                str[63] = " 1등주식은 "+B.ToString()+"% 상향이다.";
                str[64] = " 1등주식은 "+(-B).ToString()+"% 하향이다.";
                str[61] = " 2등주식과 3등주식의 폭은 "+(C-A).ToString()+"%이다.";
                str[67] = " 2등주식은 "+C.ToString()+"% 상향이다.";
                str[68] = " 2등주식은 "+(-C).ToString()+"% 하향이다.";
                str[62] = " 1등주식과 2등주식의 폭은 "+(B-C).ToString()+"%이다.";
                if(B>0){
                    str[66] = " 1등주식은 "+B.ToString()+"%의 변동을 보인다.";
                }else{
                    str[66] = " 1등주식은 "+(-B).ToString()+"%의 변동을 보인다.";
                }
                if(C>0){
                    str[70] = " 2등주식은 "+C.ToString()+"%의 변동을 보인다.";
                }else{
                    str[70] = " 2등주식은 "+(-C).ToString()+"%의 변동을 보인다.";
                }
                if(A>0){
                    str[74] = " 3등주식은 "+A.ToString()+"%의 변동을 보인다.";
                }else{
                    str[74] = " 3등주식은 "+(-A).ToString()+"%의 변동을 보인다.";
                }

            }
        }else if(B>C){
            if(A>C){//213
                str[71] = " 3등주식은 "+C.ToString()+"% 상향이다.";
                str[72] = " 3등주식은 "+(-C).ToString()+"% 하향이다.";
        
                str[60] = " 1등주식과 3등주식의 폭은 "+(B-C).ToString()+"%이다.";
                str[63] = " 1등주식은 "+B.ToString()+"% 상향이다.";
                str[64] = " 1등주식은 "+(-B).ToString()+"% 하향이다.";
                str[61] = " 2등주식과 3등주식의 폭은 "+(A-C).ToString()+"%이다.";
                str[67] = " 2등주식은 "+A.ToString()+"% 상향이다.";
                str[68] = " 2등주식은 "+(-A).ToString()+"% 하향이다.";
                str[62] = " 1등주식과 2등주식의 폭은 "+(B-A).ToString()+"%이다.";
                if(B>0){
                    str[66] = " 1등주식은 "+B.ToString()+"%의 변동을 보인다.";
                }else{
                    str[66] = " 1등주식은 "+(-B).ToString()+"%의 변동을 보인다.";
                }
                if(A>0){
                    str[70] = " 2등주식은 "+A.ToString()+"%의 변동을 보인다.";
                }else{
                    str[70] = " 2등주식은 "+(-A).ToString()+"%의 변동을 보인다.";
                }
                if(C>0){
                    str[74] = " 3등주식은 "+C.ToString()+"%의 변동을 보인다.";
                }else{
                    str[74] = " 3등주식은 "+(-C).ToString()+"%의 변동을 보인다.";
                }

            }else{//312
                str[71] = " 3등주식은 "+B.ToString()+"% 상향이다.";
                str[72] = " 3등주식은 "+(-B).ToString()+"% 하향이다.";
        
                str[67] = " 2등주식은 "+A.ToString()+"% 상향이다.";
                str[68] = " 2등주식은 "+(-A).ToString()+"% 하향이다.";
                str[60] = " 1등주식과 3등주식의 폭은 "+(C-B).ToString()+"%이다.";
                str[63] = " 1등주식은 "+C.ToString()+"% 상향이다.";
                str[64] = " 1등주식은 "+(-C).ToString()+"% 하향이다.";
                str[61] = " 2등주식과 3등주식의 폭은 "+(A-B).ToString()+"%이다.";
                if(C>0){
                    str[66] = " 1등주식은 "+C.ToString()+"%의 변동을 보인다.";
                }else{
                    str[66] = " 1등주식은 "+(-C).ToString()+"%의 변동을 보인다.";
                }
                if(A>0){
                    str[70] = " 2등주식은 "+A.ToString()+"%의 변동을 보인다.";
                }else{
                    str[70] = " 2등주식은 "+(-A).ToString()+"%의 변동을 보인다.";
                }
                if(B>0){
                    str[74] = " 3등주식은 "+B.ToString()+"%의 변동을 보인다.";
                }else{
                    str[74] = " 3등주식은 "+(-B).ToString()+"%의 변동을 보인다.";
                }

                str[62] = " 1등주식과 2등주식의 폭은 "+(C-A).ToString()+"%이다.";

            }
        }else{//321
            str[60] = " 1등주식과 3등주식의 폭은 "+(C-A).ToString()+"%이다.";
            str[63] = " 1등주식은 "+C.ToString()+"% 상향이다.";
            str[64] = " 1등주식은 "+(-C).ToString()+"% 하향이다.";
            str[67] = " 2등주식은 "+B.ToString()+"% 상향이다.";
            str[68] = " 2등주식은 "+(-B).ToString()+"% 하향이다.";
            str[61] = " 2등주식과 3등주식의 폭은 "+(B-A).ToString()+"%이다.";
            str[71] = " 3등주식은 "+A.ToString()+"% 상향이다.";
            str[72] = " 3등주식은 "+(-A).ToString()+"% 하향이다.";
            str[62] = " 1등주식과 2등주식의 폭은 "+(B-C).ToString()+"%이다.";
            if(C>0){
                str[66] = " 1등주식은 "+C.ToString()+"%의 변동을 보인다.";
            }else{
                str[66] = " 1등주식은 "+(-C).ToString()+"%의 변동을 보인다.";
            }
            if(B>0){
                str[70] = " 2등주식은 "+B.ToString()+"%의 변동을 보인다.";
            }else{
                str[70] = " 2등주식은 "+(-B).ToString()+"%의 변동을 보인다.";
            }
            if(A>0){
                str[74] = " 3등주식은 "+A.ToString()+"%의 변동을 보인다.";
            }else{
                str[74] = " 3등주식은 "+(-A).ToString()+"%의 변동을 보인다.";
            }

        }
   
        str[65] = " 1등주식은 동결이다.";
        str[69] = " 2등주식은 동결이다.";
        str[73] = " 3등주식은 동결이다.";
        str[75] = " 100%성장하는 회사가 존재한다.";
        str[76] = " 100%성장하는 회사는 존재하지 않는다.";
        str[77] = " 80%이상 성장하는 회사가 존재한다.";
        str[78] = " 80%이상 성장하는 회사는 존재하지 않는다.";

        str[79] = " 50%이상 성장하는 회사가 존재한다.";
        str[80] = " 50%이상 성장하는 회사는 존재하지 않는다.";
        str[81] = " 30%이상 성장하는 회사가 존재한다.";
        str[82] = " 30%이상 성장하는 회사는 존재하지 않는다.";
        str[83] = " 20%이하 성장하는 회사가 존재한다.";
        str[84] = " 20%이하 성장하는 회사는 존재하지 않는다.";

        str[85] = " 100%하향하는 회사가 존재한다.";
        str[86] = " 100%하향하는 회사는 존재하지 않는다.";
        str[87] = " 80%이상 하향하는 회사가 존재한다.";
        str[88] = " 80%이상 하향하는 회사는 존재하지 않는다.";

        str[89] = " 50%이상 하향하는 회사가 존재한다.";
        str[90] = " 50%이상 하향하는 회사는 존재하지 않는다.";
        str[91] = " 30%이상 하향하는 회사가 존재한다.";
        str[92] = " 30%이상 하향하는 회사는 존재하지 않는다.";
        str[93] = " 20%이하 하향하는 회사가 존재한다.";
        str[94] = " 20%이하 하향하는 회사는 존재하지 않는다.";

        str[95] = " 같은 수준의 성장을 보인 회사가 존재한다.";
        str[96] = " 같은 수준의 하향을 보인 회사가 존재한다.";
        str[97] = " 동결한 회사가 2개이다.";

        str[98] = " A회사가 "+A.ToString()+"%의 변동을 보인다.";
        str[99] = " B회사가 "+B.ToString()+"%의 변동을 보인다.";
        str[100] = " C회사가 "+C.ToString()+"%의 변동을 보인다.";
        if(A>B){
            str[101] = " A회사와 B회사의 변동차는 "+(A-B).ToString()+"%이다.";
        }else{
            str[101] = "  A회사와 B회사의 변동차는 "+(B-A).ToString()+"%이다.";
        }
        if(A>C){
            str[102] = " A회사와 C회사의 변동차는 "+(A-C).ToString()+"%이다.";
        }else{
            str[102] = "  A회사와 C회사의 변동차는 "+(C-A).ToString()+"%이다.";
        }
        if(B>C){
            str[103] = " B회사와 C회사의 변동차는 "+(B-C).ToString()+"%이다.";
        }else{
            str[103] = "  B회사와 C회사의 변동차는 "+(C-B).ToString()+"%이다.";
        }
        a=2;
    }
    if(a==2){
        if(D>0){
            arr[1]=1;
        }else if(D<0){
            arr[2]=1;
        }else if(D==0){
            arr[3]=1;
        }
        if(A>0){
            var R1 = Random.Range(1,4);
            if(R1==1){
                arr[4]=1;
            }else if(R1==2){
                arr[7]=1;
            }else if(R1==3){
                arr[9]=1;
            }
            arr[98]=1;
        }else if(A<0){
            var R2 = Random.Range(1,4);
            if(R2==1){
                arr[5]=1;
            }else if(R2==2){
                arr[6]=1;
            }else if(R2==3){
                arr[9]=1;
            }
            arr[98]=1;
        }else if(A==0){
            var R3 = Random.Range(1,4);
            if(R3==1){
                arr[5]=1;
            }else if(R3==2){
                arr[7]=1;
            }else if(R3==3){
                arr[8]=1;
            }
        }
        if(B>0){
            var R4 = Random.Range(1,4);
            if(R4==1){
                arr[10]=1;
            }else if(R4==2){
                arr[13]=1;
            }else if(R4==3){
                arr[15]=1;
            }
            arr[99]=1;
        }else if(B<0){
            var R5 = Random.Range(1,4);
            if(R5==1){
                arr[11]=1;
            }else if(R5==2){
                arr[12]=1;
            }else if(R5==3){
                arr[15]=1;
            }
            arr[99]=1;
        }else if(B==0){
            var R6 = Random.Range(1,4);
            if(R6==1){
                arr[11]=1;
            }else if(R6==2){
                arr[13]=1;
            }else if(R6==3){
                arr[14]=1;
            }
        }
        if(C>0){
            var R7 = Random.Range(1,4);
            if(R7==1){
                arr[16]=1;
            }else if(R7==2){
                arr[19]=1;
            }else if(R7==3){
                arr[21]=1;
            }
            arr[100]=1;
        }else if(C<0){
            var R8 = Random.Range(1,4);
            if(R8==1){
                arr[17]=1;
            }else if(R8==2){
                arr[18]=1;
            }else if(R8==3){
                arr[21]=1;
            }
            arr[100]=1;
        }else if(C==0){
            var R9 = Random.Range(1,4);
            if(R9==1){
                arr[17]=1;
            }else if(R9==2){
                arr[19]=1;
            }else if(R9==3){
                arr[20]=1;
            }
        }
        if(A>B){
            if(A>C){//A가 1등
                if(B>C){//abc 123
                    var R22 = Random.Range(1,4);
                    if(R22==1){
                        arr[22]=1;
                        arr[29]=1;
                        arr[36]=1;
                    }else if(R22==2){
                        arr[26]=1;
                        arr[31]=1;
                        arr[38]=1;
                    }else if(R22==3){
                        arr[27]=1;
                        arr[33]=1;
                        arr[37]=1;
                    }
                    if(A>0){
                   
                        arr[63]=1;
                        arr[66]=1;
                    }else if(A<0){
                        arr[64]=1;
                        arr[66]=1;
                    }else if(A==0){
                        arr[65]=1;
                    }
                    if(B>0){
                        arr[67]=1;
                        arr[70]=1;
                    }else if(B<0){
                        arr[68]=1;
                        arr[70]=1;
                    }else if(B==0){
                        arr[69]=1;
                    }
                    if(C>0){
                        arr[71]=1;
                        arr[74]=1;
                    }else if(C<0){
                        arr[72]=1;
                        arr[74]=1;
                    }else if(C==0){
                        arr[73]=1;
                    }

                }else {//132
                    var R23 = Random.Range(1,4);
                    if(R23==1){
                        arr[22]=1;
                        arr[30]=1;
                        arr[35]=1;
                    }else if(R23==2){
                        arr[26]=1;
                        arr[31]=1;
                        arr[39]=1;
                    }else if(R23==3){
                        arr[27]=1;
                        arr[32]=1;
                        arr[37]=1;
                    }
               
               
                
                    if(A>0){
                        arr[63]=1;
                        arr[65]=1;
                    }else if(A<0){
                        arr[64]=1;
                        arr[65]=1;
                    }else if(A==0){
                        arr[65]=1;
                    }
                    if(C>0){
                        arr[67]=1;
                        arr[70]=1;
                    }else if(C<0){
                        arr[68]=1;
                        arr[70]=1;
                    }else if(C==0){
                        arr[69]=1;
                    }
                    if(B>0){
                        arr[71]=1;
                        arr[74]=1;
                    }else if(B<0){
                        arr[72]=1;
                        arr[74]=1;
                    }else if(B==0){
                        arr[73]=1;
                    }
                }
            }else {//231

                if(C>0){
                    arr[63]=1;
                    arr[65]=1;
                }else if(C<0){
                    arr[64]=1;
                    arr[65]=1;
                }else if(C==0){
                    arr[65]=1;
                }
                if(A>0){
                    arr[67]=1;
                    arr[70]=1;
                }else if(A<0){
                    arr[68]=1;
                    arr[70]=1;
                }else if(A==0){
                    arr[69]=1;
                }
                if(B>0){
                    arr[71]=1;
                    arr[74]=1;
                }else if(B<0){
                    arr[72]=1;
                    arr[74]=1;
                }else if(B==0){
                    arr[73]=1;
                }
                var R24 = Random.Range(1,4);
                if(R24==1){
                    arr[23]=1;
                    arr[30]=1;
                    arr[34]=1;
                }else if(R24==2){
                    arr[25]=1;
                    arr[31]=1;
                    arr[38]=1;
                }else if(R24==3){
                    arr[27]=1;
                    arr[32]=1;
                    arr[39]=1;
                }
            
            }
        }else if(B>C){
            if(A>C){//213
                if(B>0){
                    arr[63]=1;
                    arr[65]=1;
                }else if(B<0){
                    arr[64]=1;
                    arr[65]=1;
                }else if(B==0){
                    arr[65]=1;
                }
                if(A>0){
                    arr[67]=1;
                    arr[70]=1;
                }else if(A<0){
                    arr[68]=1;
                    arr[70]=1;
                }else if(A==0){
                    arr[69]=1;
                }
                if(C>0){
                    arr[71]=1;
                    arr[74]=1;
                }else if(C<0){
                    arr[72]=1;
                    arr[74]=1;
                }else if(C==0){
                    arr[73]=1;
                }
                var R25 = Random.Range(1,4);
                if(R25==1){
                    arr[23]=1;
                    arr[28]=1;
                    arr[36]=1;
                }else if(R25==2){
                    arr[25]=1;
                    arr[32]=1;
                    arr[38]=1;
                }else if(R25==3){
                    arr[27]=1;
                    arr[33]=1;
                    arr[37]=1;
                }
      
            }else{//312
                if(B>0){
                    arr[63]=1;
                    arr[65]=1;
                }else if(B<0){
                    arr[64]=1;
                    arr[65]=1;
                }else if(B==0){
                    arr[65]=1;
                }
                if(C>0){
                    arr[67]=1;
                    arr[70]=1;
                }else if(C<0){
                    arr[68]=1;
                    arr[70]=1;
                }else if(C==0){
                    arr[69]=1;
                }
                if(A>0){
                    arr[71]=1;
                    arr[74]=1;
                }else if(A<0){
                    arr[72]=1;
                    arr[74]=1;
                }else if(A==0){
                    arr[73]=1;
                }
                var R26 = Random.Range(1,4);
                if(R26==1){
                    arr[24]=1;
                    arr[28]=1;
                    arr[35]=1;
                }else if(R26==2){
                    arr[25]=1;
                    arr[33]=1;
                    arr[39]=1;
                }else if(R26==3){
                    arr[26]=1;
                    arr[32]=1;
                    arr[37]=1;
                }
      
            }
        }else {//321
            if(C>0){
                arr[63]=1;
                arr[65]=1;
            }else if(C<0){
                arr[64]=1;
                arr[65]=1;
            }else if(C==0){
                arr[65]=1;
            }
            if(B>0){
                arr[67]=1;
                arr[70]=1;
            }else if(B<0){
                arr[68]=1;
                arr[70]=1;
            }else if(B==0){
                arr[69]=1;
            }
            if(A>0){
                arr[71]=1;
                arr[74]=1;
            }else if(A<0){
                arr[72]=1;
                arr[74]=1;
            }else if(A==0){
                arr[73]=1;
            }
            var R27 = Random.Range(1,4);
            if(R27==1){
                arr[24]=1;
                arr[29]=1;
                arr[34]=1;
            }else if(R27==2){
                arr[25]=1;
                arr[31]=1;
                arr[38]=1;
            }else if(R27==3){
                arr[26]=1;
                arr[33]=1;
                arr[39]=1;
            }
      
        }
        if((A!=0)&&(B!=0)&&(C!=0)){
            arr[40]=1;
        }
        if((A<0)&&(B<0)&&(C<0)){
            arr[41]=1;
        }
        if((A>0)&&(B>0)&&(C>0)){
            arr[42]=1;
        }
        if(( arr[40]==1)&&( arr[41]==1)){
            var R40 = Random.Range(1,3);
            if(R40==1){
                arr[40]=0;
            }else if(R40==2){
                arr[41]=0;
            }
        }
        if(( arr[40]==1)&&( arr[42]==1)){
            var R41 = Random.Range(1,3);
            if(R41==1){
                arr[40]=0;
            }else if(R41==2){
                arr[42]=0;
            }
        }
        //////////////////
        if(A>B){
            var R47 = Random.Range(1,3);
            if(R47==1){
                arr[47]=1;
            }else if(R47==2){
                arr[48]=1;
            }
        }else{
            var R48 = Random.Range(1,3);
            if(R48==1){
                arr[53]=1;
            }else if(R48==2){
                arr[54]=1;
            }
    
        }
        if(A>C){
            var R49 = Random.Range(1,3);
            if(R49==1){
                arr[49]=1;
            }else if(R49==2){
                arr[50]=1;
            }

        }else{
            var R50 = Random.Range(1,3);
            if(R50==1){
                arr[55]=1;
            }else if(R50==2){
                arr[56]=1;
            }
     
        
        }
        if(B>C){
            var R51 = Random.Range(1,3);
            if(R51==1){
                arr[51]=1;
            }else if(R51==2){
                arr[52]=1;
            }
    
        }else{
            var R52 = Random.Range(1,3);
            if(R52==1){
                arr[57]=1;
            }else if(R52==2){
                arr[58]=1;
            }
     
        }
        arr[59] =1;
        arr[60] =1;
        arr[61] =1;
        arr[62] =1;
        if((A==100)||(B==100)||(C==100)){
            arr[75] =1;
        
        }else{
            arr[76] =1;
        }
        if((A>80)||(B>80)||(C>80)){
            arr[77] =1;
        }else{
            arr[78] =1;
        }
        if(( arr[75]==1)&&( arr[77]==1)){
            var R75 = Random.Range(1,3);
            if(R75==1){
                arr[75]=0;
            }else if(R75==2){
                arr[77]=0;
            }
        }
        if(( arr[76]==1)&&( arr[78]==1)){
            var R76 = Random.Range(1,3);
            if(R76==1){
                arr[76]=0;
            }else if(R76==2){
                arr[78]=0;
            }
        }
        if((A>50)||(B>50)||(C>50)){
            arr[79] =1;
        }else{
            arr[80] =1;
        }
        if((A>30)||(B>30)||(C>30)){
            arr[81] =1;
        }else{
            arr[82] =1;
        }
        if(( arr[79]==1)&&( arr[81]==1)){
            var R79 = Random.Range(1,3);
            if(R79==1){
                arr[79]=0;
            }else if(R79==2){
                arr[81]=0;
            }
        }
        if(( arr[80]==1)&&( arr[82]==1)){
            var R80 = Random.Range(1,3);
            if(R80==1){
                arr[80]=0;
            }else if(R80==2){
                arr[82]=0;
            }
        }
        if(((A<20)&&(A>0))||((B<20)&&(B>0))||((C<20)&&(C>0))){
            arr[83] =1;
        }else{
            arr[84] =1;
        }
        if((A==-100)||(B==-100)||(C==-100)){
            arr[85] =1;
        
        }else{
            arr[86] =1;
        }
        if((A<-80)||(B<-80)||(C<-80)){
            arr[87] =1;
        }else{
            arr[88] =1;
        }
        if(( arr[85]==1)&&( arr[87]==1)){
            var R85 = Random.Range(1,3);
            if(R85==1){
                arr[85]=0;
            }else if(R85==2){
                arr[87]=0;
            }
        }
        if(( arr[86]==1)&&( arr[88]==1)){
            var R86 = Random.Range(1,3);
            if(R86==1){
                arr[86]=0;
            }else if(R86==2){
                arr[88]=0;
            }
        }
        if((A<-50)||(B<-50)||(C<-50)){
            arr[89] =1;
        }else{
            arr[90] =1;
        }
        if((A<-30)||(B<-30)||(C<-30)){
            arr[91] =1;
        }else{
            arr[92] =1;
        }
        if(( arr[89]==1)&&( arr[91]==1)){
            var R89 = Random.Range(1,3);
            if(R89==1){
                arr[89]=0;
            }else if(R89==2){
                arr[91]=0;
            }
        }
        if(( arr[90]==1)&&( arr[92]==1)){
            var R90 = Random.Range(1,3);
            if(R90==1){
                arr[90]=0;
            }else if(R90==2){
                arr[92]=0;
            }
        }
        if(((A>-20)&&(A<0))||((B>-20)&&(B<0))||((C>-20)&&(C<0))){
            arr[93] =1;
        }else{
            arr[94] =1;
        }
        arr[101] =1;
        arr[102] =1;
         arr[103] =1;
        if(((A==0)&&(B==0)&&(C!=0))||((A==0)&&(B!=0)&&(C==0))||((A!=0)&&(B==0)&&(C==0))){
            arr[97] =1;
        }
        if(((A==B)&&(B==C)&&(A>0))||((A==B)&&(B!=C)&&(A>0))||((A!=B)&&(B==C)&&(B>0))||((A==C)&&(B!=C)&&(C>0))){
            arr[95] =1;
        }
        if(((A==B)&&(B==C)&&(A<0))||((A==B)&&(B!=C)&&(A<0))||((A!=B)&&(B==C)&&(B<0))||((A==C)&&(B!=C)&&(C<0))){
            arr[96] =1;
        }
        if(((A>0)&&(B<0)&&(C<0))||((A<0)&&(B>0)&&(C<0))||((A<0)&&(B<0)&&(C>0))){
            var R43 = Random.Range(1,3);
            if(R43==1){
                arr[43]=1;
            }else if(R43==2){
                arr[46]=1;
            }
        }
        if(((A>0)&&(B>0)&&(C<0))||((A<0)&&(B>0)&&(C>0))|((A>0)&&(B<0)&&(C>0))){
            var R44 = Random.Range(1,3);
            if(R44==1){
                arr[44]=1;
            }else if(R44==2){
                arr[45]=1;
            }
      
        }
        if(( arr[63]==1)&&( arr[66]==1)){
            var R163 = Random.Range(1,3);
            if(R163==1){
                arr[63]=0;
            }else if(R163==2){
                arr[66]=0;
            }
        } if(( arr[67]==1)&&( arr[70]==1)){
            var R167 = Random.Range(1,3);
            if(R167==1){
                arr[67]=0;
            }else if(R167==2){
                arr[70]=0;
            }
        } if(( arr[71]==1)&&( arr[74]==1)){
            var R171 = Random.Range(1,3);
            if(R171==1){
                arr[71]=0;
            }else if(R171==2){
                arr[74]=0;
            }
        }
        if(( arr[64]==1)&&( arr[66]==1)){
            var R164 = Random.Range(1,3);
            if(R164==1){
                arr[64]=0;
            }else if(R164==2){
                arr[66]=0;
            }
        } if(( arr[68]==1)&&( arr[70]==1)){
            var R168 = Random.Range(1,3);
            if(R168==1){
                arr[68]=0;
            }else if(R168==2){
                arr[70]=0;
            }
        } if(( arr[72]==1)&&( arr[74]==1)){
            var R172 = Random.Range(1,3);
            if(R172==1){
                arr[72]=0;
            }else if(R172==2){
                arr[74]=0;
            }
        }
        for(var j = 1; j<9;j++){
            var R = Random.Range(1,103);
            if(arr[R]==1){
                srr[j]=R;
                arr[R]=0;
            }
            if(srr[j]==0){
                j-=1;
            }
        }
        for(var M = 1; M<9;M++){
            sstr[M]=str[(srr[M])];
        }
       
        PlayerPrefs.SetString("Hint0",sstr[1]);
        PlayerPrefs.SetString("Hint1",sstr[2]);
        PlayerPrefs.SetString("Hint2",sstr[3]);
        PlayerPrefs.SetString("Hint3",sstr[4]);
        PlayerPrefs.SetString("Hint4",sstr[5]);
        PlayerPrefs.SetString("Hint5",sstr[6]);
        PlayerPrefs.SetString("Hint6",sstr[7]);
        PlayerPrefs.SetString("Hint7",sstr[8]);
        abc.SendMessage("Give_Hint");
        a=0;
    }
}