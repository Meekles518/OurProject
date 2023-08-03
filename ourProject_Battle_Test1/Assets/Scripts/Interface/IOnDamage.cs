using UnityEngine;

//충돌 후 데미지를 입는 타입들이 공통적으로 가져야 하는 인터페이스
public interface IOnDamage
{
    //충돌 후 데미지를 입는 타입들(플레이어, 적, 총알 등)은 이 interface를 계승한 후 onDamage를 구현한다.
    //onContact 상대의 공격력을 입력으로 받아서 계산식에 맞게 자신의 체력을 감소시키고 사망여부를 판단한다.
    void onDamage(float otherDamage);
}