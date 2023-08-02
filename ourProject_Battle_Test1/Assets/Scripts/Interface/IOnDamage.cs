using UnityEngine;

//충돌 후 데미지를 입는 타입들이 공통적으로 가져야 하는 인터페이스
public interface IOnDamage
{
    //충돌 후 데미지를 입는 타입들(플레이어, 적, 총알 등)은 이 interface를 계승한 후 onDamaget를 구현한다.
    //onContact 메서드는 체력, 자신의 공격력, 상대의 공격력을 입력으로 받는다.
    void onDamage(float otherDamage);
}

    
