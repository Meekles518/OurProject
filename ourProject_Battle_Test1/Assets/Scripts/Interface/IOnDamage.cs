using UnityEngine;

//�浹 �� �������� �Դ� Ÿ�Ե��� ���������� ������ �ϴ� �������̽�
public interface IOnDamage
{
    //�浹 �� �������� �Դ� Ÿ�Ե�(�÷��̾�, ��, �Ѿ� ��)�� �� interface�� ����� �� onDamage�� �����Ѵ�.
    //onContact ����� ���ݷ��� �Է����� �޾Ƽ� ���Ŀ� �°� �ڽ��� ü���� ���ҽ�Ű�� ������θ� �Ǵ��Ѵ�.
    void onDamage(float otherDamage);
}