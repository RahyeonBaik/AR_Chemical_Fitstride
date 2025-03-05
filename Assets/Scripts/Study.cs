using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Study : MonoBehaviour
{
    //자료형

    private int x; // 정수형 1,-1,
    private float f; // 실수형 0.1 , -0.1 
    private string s; // 문장형 "Apple" , "백라현" , "권영주"
    private char c; // 문자형 'A' , 'B' , 'C'
    private bool b; // 참/거짓 , True / False

//접근 지정자

// private : 내 클래스에서 접근 가능
// public : 내 클래스 , 타 클래스에서도 접근 가능
// protected : 자식 클래스에서만 접근 가능
// abstract : 자식 클래스 무조건 재정의를 필요로 할 때 사용하는 접근 지정자
// virtual : 자식 클래스에서 해당 함수를 추가적으로 정의할 떄 사용하는 접근 지정자

// 조건문   

// if - else if - else문
//     private void Start()
//     {
//         if (x == 0)
//         {
//             Debug.Log("X is 0");
//         }
//         else if (x == 1)
//         {
//             Debug.Log("X is 1");
//         }
//         else
//         {
//             Debug.Log("X is not 1 , 2");
//         }
//     }
// Switch문
// private void Start()
// {
//     switch (x)
//     {
//         case 0:
//             Debug.Log("X is 0");
//             break;
//         case 1:
//             Debug.Log("X is 1");
//             break;
//         default:
//             Debug.Log("X is not 1 , 2");
//             break;
//     }
// }

// 삼항연산자 
// private void Start()
// {
//     var temp1 = (x == 0) ? 10 : 20;
// }
}