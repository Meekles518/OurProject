using UnityEngine;

//�浹 �� �������� �Դ� Ÿ�Ե��� ���������� ������ �ϴ� �������̽�
public interface IOnDamage
{
    //�浹 �� �������� �Դ� Ÿ�Ե�(�÷��̾�, ��, �Ѿ� ��)�� �� interface�� ����� �� onDamaget�� �����Ѵ�.
    //onContact �޼���� ü��, �ڽ��� ���ݷ�, ����� ���ݷ��� �Է����� �޴´�.
    void onDamage(float otherDamage);
}

    
